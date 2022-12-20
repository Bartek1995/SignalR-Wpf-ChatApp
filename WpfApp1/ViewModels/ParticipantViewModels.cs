using System.Collections.Generic;
using System.Windows;
using Chat.Models;
using Microsoft.AspNetCore.SignalR.Client;
using WpfApp1.Models;
using WpfApp1.Mvvm;

namespace WpfApp1.ViewModels;

public class ParticipantViewModels : ViewModelBase
{
    public SignalRClient chat_client;
    public string LoginTextBox { get; set; }
    //public List<Message> Messages = new List<Message>();
    //public List<Message> Messages { get; set; } = new List<Message>();
    //public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
    private List<Message> _Messages;
    public List<Message> Messages
    {
        get
        {
            return _Messages;
        }
        set
        {
            _Messages = value;
            OnPropertyChanged("Messages");
        }
    }
    
    public ParticipantViewModels()
    {
        AddFishButton = new Command(_AddFishButton);
        LogoutButton = new Command(_LogoutButton);
        chat_client = new SignalRClient("user1", "123");
        PrepareMessages();
    }

    public Command AddFishButton { get; }
    public Command LogoutButton { get; }

    public async void _AddFishButton()
    {

        OnPropertyChanged(nameof(Messages));
    }

    public async void _LogoutButton()
    {
        (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
    }

    public async void PrepareMessages()
    {
        var m = new Message("test", "Testowa wiadomość");
        Messages = new List<Message>();
        Messages.Add(m);
        Messages.Add(new Message("fasdfas", "fsdfsd"));

        chat_client.hubConnection.On<Message>("ReceiveMessage", msg =>
        {
            string message = $"{msg.Username}: {msg.Content}";
            MessageBox.Show($"{msg.Username}: {msg.Content}");
            Messages.Add(msg);
            OnPropertyChanged(nameof(Messages));
        });

        chat_client.hubConnection.On<string>("HelloMessage", msg =>
        {
            MessageBox.Show($"User {msg} joined chat");
            //Messages.Add(msg);
            OnPropertyChanged(nameof(Messages));
        });

        await chat_client.hubConnection.StartAsync();

        await chat_client.hubConnection.SendAsync("SayHello", chat_client.username);
    }
    
}