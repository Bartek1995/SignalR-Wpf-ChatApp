using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.SignalR.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows;

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
            //var base64 = Convert.ToBase64String("user1:123"u8);
            this.hubConnection = new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.Headers.Add("Authorization", $"Bear {base64}");
                })
            .WithAutomaticReconnect()
                .Build();

        }

        public async void MessagesManagement()
        {
            MessageBox.Show(this.username);
            this.hubConnection.On<Message>("ReceiveMessage", msg =>
            {
                Console.WriteLine($"{msg.Username}: {msg.Content}");
                MessageBox.Show($"{msg.Username}: {msg.Content}");
            });

            this.hubConnection.On<string>("HelloMessage", msg =>
            {
                Console.WriteLine($"User {msg} joined chat");
                MessageBox.Show($"User {msg} joined chat");
            });


            await this.hubConnection.StartAsync();

            await this.hubConnection.SendAsync("SayHello", this.username);

        }
    }
}