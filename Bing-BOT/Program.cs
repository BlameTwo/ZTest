using Bing_BOT.Services;
using Bing_BOT.Services.Contract;

ICreateBingBot bot = new CreateBingBot();

var cookie = "1yuwKdgcIdRcHkp3YAIYQzcN_qSbctmKR-iQ8usJtT5NYLtj6aeVTZ5_9yPlzIybeUDeEt1KC4PKe1SZgjKA9a6gN0UludSZIBbu1WTUupHYuyneeyklJaa5k3woaTTzXon9Wk3PUqr5Hzq9ipkbzCQwRu2b0mfUePEmYkw1Q2FloQK_jhVV0uuDPI5hOKSyw1zDYExvlIoI7MrELJHfhFFIa47gq7wIg8ashh9ZgjBs";

var chatCts = new CancellationTokenSource();

var result = await bot.CreateBingConversation(cookie,chatCts.Token);


IBingChatClient client = new BingChatClient();
client.Init(new Bing_BOT.Models.BingChatOption( Bing_BOT.Enum.CharStyle.Creative,cookie,1000));

await client.ChatAsync(
    new Bing_BOT.Models.BingRequest("你好，bing") { Session = new Bing_BOT.Models.ConversationSession(0,result.conversationId,result.clientId,result.conversationSignature)}
    ,chatCts.Token);

Console.WriteLine(result.clientId);