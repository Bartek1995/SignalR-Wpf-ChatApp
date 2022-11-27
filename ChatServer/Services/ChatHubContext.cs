using Chat.Models;
using ChatServer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Services
{
    public class ChatHubContext
    {
        private readonly IHubContext<ChatHub> hubContext;

        public ChatHubContext(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }


        public void SendAll(string message)
        {
            hubContext.Clients.All.SendAsync("ReceiveMessage", new Message
            {
                Content = message,
                Username = "SERVER"
            });
        }
    }
}
