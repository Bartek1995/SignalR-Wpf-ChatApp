using Models;
using WpfApp1.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using System.Windows.Input;
using System.Security.Cryptography;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public Command LoginTXTBox { get; }
        public Command ReverseButton { get; }
        public string LoginTextBox { get; set; }
        public string PasswordTextBox { get; set; }
        public LoginViewModel()
        {
            LoginTXTBox = new Command(_LoginTXTBox);
            ReverseButton = new Command(_ReverseButton);
            
        }

        private string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        public async void _LoginTXTBox()
        {
            if(String.IsNullOrEmpty(PasswordTextBox) || String.IsNullOrEmpty(LoginTextBox)){
                MessageBox.Show("Brakuje danych");
            }
            else
            {
                //MessageBox.Show(LoginTextBox + " " + PasswordTextBox);
                var Api = new ApiService();
                PasswordTextBox = hashPassword(PasswordTextBox);
                var user = await Api.GetPerson(LoginTextBox, PasswordTextBox);
                if (user == null)
                {
                    MessageBox.Show("Błędny login lub hasło");
                }
                else
                {
                    Properties.Settings.Default.IsLogedIn = true;
                    Properties.Settings.Default.UserID = Convert.ToInt32(user.Id);
                    Properties.Settings.Default.UserRole = user.Role;


                    MessageBox.Show("Witaj " + user.FirstName + " o ID " + (Properties.Settings.Default.UserID).ToString());
                    //TUTAJ SWITCH OKNA NA OKNO PO ZALOGOWANIU
                    if (user.Role == "Juror")
                    {
                        (Application.Current as App).viewModel.selectedViewModel = new RateViewModel();
                    }
                    else
                    {
                        (Application.Current as App).viewModel.selectedViewModel = new ParticipantViewModels();

                    }
                }
            }
            
        }
        public async void _ReverseButton()
        {
            (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
        }
    }
}
