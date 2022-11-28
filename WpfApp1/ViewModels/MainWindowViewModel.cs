using System.Windows;
using WpfApp1.Mvvm;
using WpfApp1.Properties;

namespace WpfApp1.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        //RESET DEFAULT SETTINGS
        Settings.Default.IsLogedIn = false;
        Settings.Default.UserID = 0;
        Settings.Default.UserRole = "null";
        Settings.Default.Save();

        GoToLoginView = new Command(_GoToLoginView);
        GoToRegistrationView = new Command(_GoToRegistrationView);
    }

    public Command GoToLoginView { get; }
    public Command GoToRegistrationView { get; }

    public async void _GoToLoginView()
    {
        (Application.Current as App).viewModel.selectedViewModel = new LoginViewModel();
    }

    public async void _GoToRegistrationView()
    {
        (Application.Current as App).viewModel.selectedViewModel = new RegistrationViewModel();
    }
}