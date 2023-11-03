using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Bound.Tablet.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task BroadcastMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendToOthers(string message)
        {
            await Clients.Others.ReceiveMessage(GetMessageToSend(message));
        }

        private string GetMessageToSend(string originalMessage)
        {
            return $"User connection id: {Context.ConnectionId}. Message: {originalMessage}";
        }

        public async Task SendToCaller(string message)
        {
            await Clients.Caller.ReceiveMessage(GetMessageToSend(message));
        }

        public async Task SendToIndividual(string connectionId, string message)
        {
            await Clients.Client(connectionId).ReceiveMessage(GetMessageToSend(message));
        }
    }
    public interface IChatHub
    {
        Task ReceiveMessage(string message);
        Task BroadcastMessage(string message);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception exception);
        Task SendToCaller(string message);
        Task SendToIndividual(string connectionId, string message);
        Task SendToOthers(string message);
    }

}

