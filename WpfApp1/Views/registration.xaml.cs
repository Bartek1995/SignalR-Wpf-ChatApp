using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views;

public partial class registration : UserControl
{
    public registration()
    {
        InitializeComponent();
        DataContext = new RegistrationViewModel();
    }
}