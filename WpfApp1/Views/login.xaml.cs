using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views;

public partial class login : UserControl
{
    public login()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }
}