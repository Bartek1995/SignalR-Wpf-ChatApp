using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Services;
using Models;
using WpfApp1.Mvvm;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Security.Cryptography;

namespace WpfApp1.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public Command CreateButton_Click { get; }
        public Command ReverseButton { get; }

        public string NameTxtBox { get; set; }
        public string SurnameTxtBox { get; set; }
        public string PasswordTxtBox { get; set; }
        public ComboBoxItem SelectedCombo { get; set; }


        private string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        public RegistrationViewModel()
        {
            CreateButton_Click = new Command(_CreateButton_Click);
            ReverseButton = new Command(_ReverseButton);

        }


        private async void _CreateButton_Click()
        {
            
            if (String.IsNullOrEmpty(NameTxtBox) || String.IsNullOrEmpty(SurnameTxtBox) || String.IsNullOrEmpty(PasswordTxtBox) )
            {
                MessageBox.Show("Brakuje danych");
            }
            else
            {
                var api = new ApiService();
                PasswordTxtBox = hashPassword(PasswordTxtBox);
                var create_result = await api.PostPerson(NameTxtBox, SurnameTxtBox, PasswordTxtBox, SelectedCombo.Content.ToString());

                if (create_result)
                {
                    (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
                }
                else
                {
                    MessageBox.Show("Błąd podczas tworzenia użytkownika, sprawdź poprawność połączenia z bazą danych");
                }
            }        
        }
        public async void _ReverseButton()
        {
            (Application.Current as App).viewModel.selectedViewModel = new MainWindowViewModel();
        }
    }
}
