﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Chat.Models;
using ConsoleApp1;
using Microsoft.AspNetCore.SignalR.Client;
using WpfApp1.Models;
using WpfApp1.Mvvm;
using WpfApp1.Properties;

namespace WpfApp1.ViewModels;

public class ParticipantViewModels : ViewModelBase
{
    public ParticipantViewModels()
    {
        AddFishButton = new Command(_AddFishButton);
        LogoutButton = new Command(_LogoutButton);
        PrepareMessages();
    }

    public Command AddFishButton { get; }
    public Command LogoutButton { get; }

    public List<Message> Messages { get; set; }

    public async void _AddFishButton()
    {
        
        // (Application.Current as App).viewModel.selectedViewModel = new AddFishViewModel();
    }

    public async void _LogoutButton()
    {
        (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
    }

    public void PrepareMessages()
    {
        var m = new Message("test", "Testowa wiadomość");
        Messages = new List<Message> { m };

        var connection = new Client().CreateConnection();
        
        connection.On<Message>("ReceiveMessage", msg =>
        {
            
            Console.WriteLine($"{msg.Username}: {msg.Content}");
        });
        
        OnPropertyChanged(nameof(Messages));
    }
    
    
}