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
    public class AppViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel = new MainWindowViewModel();
        public ViewModelBase selectedViewModel
        {
            get { return _selectedViewModel; }
            set 
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(selectedViewModel));
            }
        }
    }
}
