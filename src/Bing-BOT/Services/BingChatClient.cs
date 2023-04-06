using Bing_BOT.Models;
using Bing_BOT.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing_BOT.Services
{
    public partial class BingChatClient : IBingChatClient
    {
        BingChatOption _option;
        private bool isStart = true;

        public void Init(BingChatOption option)
        {
            _option = option;
        }


        private BingMessageChangedHandle BingMessageChangedHandle { get; set; }

        /// <summary>
        /// 消息更改事件
        /// </summary>
        public event BingMessageChangedHandle MessageChanged
        {
            add { BingMessageChangedHandle += value; }
            remove { BingMessageChangedHandle -= value; }
        }


        public async Task ChatAsync(BingRequest message, CancellationToken cancellationToken)
        {
            var bingRequest = new NetworkRequest()
            {
                invocationId = message.Session.InvocationId.ToString(),
                arguments = new()
                {
                    new()
                    {
                        conversationId = message.Session.ConversationId,
                        optionsSets =ChatHelper.GetDefaultOptions(_option.ChatStyle),
                        allowedMessageTypes =ChatHelper. GetDefaultResponseType(),
                        sliceIds = ChatHelper.GetDefaultSlids(),
                        isStartOfSession = isStart,
                        Message = new NetworkMessage()
                        {
                            text = message.message,
                            timestamp = DateTimeOffset.Now
                        },
                        participant = new ParticipantModel()
                        {
                            id = message.Session.ClientId
                        },
                        conversationSignature = message.Session.Signature
                    }
                }
            };
            var ws = await CreateNewChatHub(cancellationToken);
            await Handshake(ws, cancellationToken);
            await SendMessageAsync(ws, bingRequest, cancellationToken);
            await Receive(ws, cancellationToken);
            isStart = false;
        }

        private async Task Handshake(WebSocket ws, CancellationToken cancellationToken)
        {
            await SendMessageAsync(ws, new
            {
                protocol = "json",
                version = 1
            }, cancellationToken);
            await Receive(ws, cancellationToken);
        }

        private async Task<WebSocket> CreateNewChatHub(CancellationToken token)
        {
            var ws = new ClientWebSocket();
            var uri = new Uri("wss://sydney.bing.com/sydney/ChatHub");
            await ws.ConnectAsync(uri, token);
            return ws;
        }

        private async Task<string> Receive(WebSocket ws, CancellationToken cancellationToken)
        {

            var lastText = "";
            var i = 0;
            while (ws.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result;
                var messages = new StringBuilder();
                do
                {
                    var buffer = new byte[1024];
                    result = await ws.ReceiveAsync(buffer, cancellationToken);
                    var messageJson = Encoding.UTF8.GetString(buffer.Take(result.Count).ToArray());
                    messages.Append(messageJson);
                } while (result is { EndOfMessage: false, MessageType: WebSocketMessageType.Text } && !cancellationToken.IsCancellationRequested);
                var objects = messages.ToString().Split("\u001e", StringSplitOptions.RemoveEmptyEntries);
                if (objects.Length <= 0)
                    continue;
                var responseMsg = objects[0];
                var response = JsonSerializer.Deserialize<NetworkFixedResponse>(responseMsg)!;

                switch (response.Type)
                {
                    case 6:
                        await SendMessageAsync(ws, new
                        {
                            type = 6
                        }, cancellationToken);
                        continue;
                    case 1:
                        if (response is not { Arguments.Count: > 0 } || response.Arguments[0] is not { Messages.Count: > 0 })
                            continue;
                        var message = response.Arguments[0].Messages[0];
                        if (message.Author != "bot" || string.IsNullOrEmpty(message.Text))
                            continue;
                        message.Text = Regex
                            .Replace(message.Text, @"\[[^\]]+\]", "", RegexOptions.Compiled | RegexOptions.Multiline);
                        var thisText = message.Text.Length >= lastText.Length
                            ? message.Text[lastText.Length..]
                            : message.Text;
                        lastText = message.Text;
                        var init = i++ <= 0;
                        var end = thisText == "";
                        this.BingMessageChangedHandle.Invoke(this, new BingChangedArgs(init,end,thisText),true);
                        if (end)
                        {
                            return string.IsNullOrEmpty(message.Text) ? lastText : message.Text;
                        }
                        break;
                    case 0:
                        //返回连接状态
                        return responseMsg;
                    case 2:
                        break;

                    default:
                        continue;
                }
            }

            return "";
        }
        private async Task SendMessageAsync(WebSocket ws, object msg, CancellationToken cancellationToken)
        {
            var msgSend = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msg) + "\u001e");
            await ws.SendAsync(msgSend, WebSocketMessageType.Text, true, cancellationToken);
        }

    }
}
