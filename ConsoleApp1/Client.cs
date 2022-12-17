using Chat.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleApp1;

public class Client
{
    public  HubConnection CreateConnection()
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        var url = "http://127.0.0.1:5000/chat";
        var base64 = Convert.ToBase64String("user1:123"u8);
        var connection = new HubConnectionBuilder()
            .WithUrl(url, options =>
            {
                options.Headers.Add("Authorization", $"Bear {base64}");
            })
            .WithAutomaticReconnect()
            .Build();
        return connection;
        
        // connection.On<Message>("ReceiveMessage", msg =>
        // {
        //     Console.WriteLine($"{msg.Username}: {msg.Content}");
        // });
        //
        // connection.On<string>("HelloMessage", msg =>
        // {
        //     Console.WriteLine($"User {msg} joined chat");
        // });
        //
        //
        // await connection.StartAsync();
        //
        // await connection.SendAsync("SayHello", username);
        //
        // do
        // {
        //     var line = Console.ReadLine();
        //
        //     await connection.SendAsync("SendMessage", new Message
        //     {
        //         Username = username,
        //         Content = line
        //     });
        //
        // }
        // while (true);
    }
}