using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Mvvm;


namespace WpfApp1.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    public RegistrationViewModel()
    {
        CreateButton_Click = new Command(_CreateButton_Click);
        ReverseButton = new Command(_ReverseButton);
    }

    private CsharpdbContext _context = new();
    public Command CreateButton_Click { get; }
    public Command ReverseButton { get; }
    public string NameTxtBox { get; set; } = "";
    public string PasswordTxtBox { get; set; }



    private string hashPassword(string password)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);
        return Convert.ToBase64String(hashedPassword);
    }

    private async void _CreateButton_Click()
    {
        if (string.IsNullOrEmpty(NameTxtBox) || string.IsNullOrEmpty(PasswordTxtBox))
        {
            MessageBox.Show("Brakuje danych");
        }
        else
        {
            PasswordTxtBox = hashPassword(PasswordTxtBox);

            var user = new User(NameTxtBox, PasswordTxtBox, null);
            var result = _context.Users.Add(user);
            _context.SaveChanges();

            if (result.IsKeySet)
                (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
            else
                MessageBox.Show("Błąd podczas tworzenia użytkownika, sprawdź poprawność połączenia z bazą danych");
        }
    }

    public async void _ReverseButton()
    {
        (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
    }
}