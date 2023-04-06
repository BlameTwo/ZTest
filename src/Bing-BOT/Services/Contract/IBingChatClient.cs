using Bing_BOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing_BOT.Services.Contract
{
    public interface IBingChatClient
    {
        public void Init(BingChatOption option);
        Task ChatAsync(BingRequest message, CancellationToken cancellationToken);

        event BingMessageChangedHandle MessageChanged;
    }
}
