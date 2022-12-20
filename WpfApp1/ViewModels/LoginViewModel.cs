﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Mvvm;
using WpfApp1.Properties;


namespace WpfApp1.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public LoginViewModel()
    {
        LoginTXTBox = new Command(_LoginTXTBox);
        ReverseButton = new Command(_ReverseButton);
    }

    public Command LoginTXTBox { get; }
    public Command ReverseButton { get; }
    public string LoginTextBox { get; set; }
    public string PasswordTextBox { get; set; }

    private CsharpdbContext _context = new();

    private string hashPassword(string password)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);
        return Convert.ToBase64String(hashedPassword);
    }
    
    public void _LoginTXTBox()
    {
        if (string.IsNullOrEmpty(PasswordTextBox) || string.IsNullOrEmpty(LoginTextBox))
        {
            MessageBox.Show("Brakuje danych");
        }
        else
        {
            PasswordTextBox = hashPassword(PasswordTextBox);
            var user = new User(LoginTextBox, PasswordTextBox);
            var response =
                _context.Users.FirstOrDefault(u => u.Username == LoginTextBox && u.Password == PasswordTextBox);
            if (response is null)
            {
                MessageBox.Show("Błędny login lub hasło");
            }
            else
            {
                Settings.Default.IsLogedIn = true;
                Settings.Default.UserID = Convert.ToInt32(user.Id);
                Settings.Default.UserRole = user.Role;

                MessageBox.Show("Witaj " + user.Username);

                //TUTAJ SWITCH OKNA NA OKNO PO ZALOGOWANIU
                // MessageBox.Show("");
                (Application.Current as App).viewModel.selectedViewModel = new ParticipantViewModels();
            }
        }
    }

    public async void _ReverseButton()
    {
        (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
    }
}