using System;
using System.Text;
using System.Windows;
using Chat.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace WpfApp1.Models
{
    public class SignalRClient
    {
        string url = "http://127.0.0.1:5000/chat";
        public HubConnection hubConnection;
        public string username;

        public SignalRClient(string username, string password)
        {
            this.username = username;
            string username_and_password = $"{username}:{password}";
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(username_and_password));
            this.hubConnection = new HubConnectionBuilder()
                .WithUrl(url, options => { options.Headers.Add("Authorization", $"Bear {base64}"); })
                .WithAutomaticReconnect()
                .Build();
        }
    }
}