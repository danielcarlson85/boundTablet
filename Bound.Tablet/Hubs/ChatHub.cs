using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bound.Tablet.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public async Task SendDataViaHttp(int data)
        {
            await Clients.All.SendAsync("broadcastMessage", new { data });
        }

    }
}