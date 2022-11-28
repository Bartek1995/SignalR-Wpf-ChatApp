using WpfApp1.Mvvm;

namespace WpfApp1.ViewModels;

public class AppViewModel : ViewModelBase
{
    private ViewModelBase _selectedViewModel = new MainWindowViewModel();

    public ViewModelBase selectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged(nameof(selectedViewModel));
        }
    }
}