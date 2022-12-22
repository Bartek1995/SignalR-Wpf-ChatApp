using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using Chat.Models;
using Microsoft.AspNetCore.SignalR.Client;
using WpfApp1.Models;
using WpfApp1.Mvvm;
using WpfApp1.Properties;

namespace WpfApp1.ViewModels;

public class ChatWindowViewModel : ViewModelBase
{
    public SignalRClient chat_client;
    public string TextOfMessage { get; set; }
    public List<User> Users = new List<User>();
    private ObservableCollection<Message> _Messages = new ObservableCollection<Message>();
    public ObservableCollection<Message> Messages
    {
        get { return _Messages; }

        set
        {
            OnPropertyChanged("Messages");
        }
    }
    public Command SendMessageButton { get; }
    public Command LogoutButton { get; }


    public ChatWindowViewModel(SignalRClient _client)
    {
        SendMessageButton = new Command(_SendMessageButton);
        LogoutButton = new Command(_LogoutButton);
        chat_client = _client;
        chat_client.hubConnection.On<Message>("ReceiveMessage", msg =>
        {
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
                Messages.Add(msg);
             });
        });

        chat_client.hubConnection.On<string>("HelloMessage", user =>
        {
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
            Messages.Add(new Message(user, "Dołącza do czatu"));
            });
        });



        chat_client.hubConnection.StartAsync();

        chat_client.hubConnection.SendAsync("SayHello", chat_client.username);

    }


    public async void _SendMessageButton()
    {   if (TextOfMessage is not null)
        { 
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
                Message sendMessage = new Message(chat_client.username, TextOfMessage);
                chat_client.hubConnection.SendAsync("SendMessage", sendMessage);
                sendMessage.Username = "Ty";
                Messages.Add(sendMessage);
            });

            TextOfMessage = null;
            OnPropertyChanged(nameof(TextOfMessage));
        }
        else { 
            MessageBox.Show("Podaj treść wiadomości"); 
        }

    }

    public async void _LogoutButton()
    {
        chat_client.hubConnection.On<string>("GoodbyeMessage", user =>
        {
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
                Messages.Add(new Message(user, "Opuszcza czat"));
            });
        });


        (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
    }
   
}