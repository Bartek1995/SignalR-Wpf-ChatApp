using Models;
using WpfApp1.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Services;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Command GoToLoginView { get; }
        public Command GoToRegistrationView { get; }

        public MainWindowViewModel()
        {
            //RESET DEFAULT SETTINGS
            Properties.Settings.Default.IsLogedIn = false;
            Properties.Settings.Default.UserID = 0;
            Properties.Settings.Default.UserRole = "null";
            Properties.Settings.Default.Save();

            GoToLoginView = new Command(_GoToLoginView);
            GoToRegistrationView = new Command(_GoToRegistrationView);
        }

        public async void _GoToLoginView()
        {
            (Application.Current as App).viewModel.selectedViewModel = new LoginViewModel();
        }

        public async void _GoToRegistrationView()
        {
            (Application.Current as App).viewModel.selectedViewModel = new RegistrationViewModel();
        }
    }
}
