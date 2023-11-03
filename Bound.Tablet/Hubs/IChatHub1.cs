using System;
using System.Threading.Tasks;

namespace Bound.Tablet.Hubs
{
    public interface IChatHub1
    {
        Task BroadcastMessage(string message);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync(Exception exception);
        Task SendToCaller(string message);
        Task SendToIndividual(string connectionId, string message);
        Task SendToOthers(string message);
    }
}