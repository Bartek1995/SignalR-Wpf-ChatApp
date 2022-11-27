using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message msg)
        {
            await Clients.Others.SendAsync("ReceiveMessage", msg);
        }

        public async Task SayHello(string username)
        {
            await Clients.Others.SendAsync("HelloMessage", username);
        }

    }
}
