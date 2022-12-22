using System;
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
        PasswordTextBox = hashPassword(PasswordTextBox);

        if (string.IsNullOrEmpty(PasswordTextBox) || string.IsNullOrEmpty(LoginTextBox))
        {
            MessageBox.Show("Brakuje danych");
        }
        else
        {
            var user = _context.Users.FirstOrDefault(user => user.Username == LoginTextBox && user.Password == PasswordTextBox);
            if (user is not null)
            {
                SignalRClient client = new SignalRClient(LoginTextBox, PasswordTextBox);
                (Application.Current as App).viewModel.selectedViewModel = new ChatWindowViewModel(client);
            }
            else
            {
                MessageBox.Show("Nieprawidłowy login lub hasło");
            }
                

        }
    }

    public async void _ReverseButton()
    {
        (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
    }
}