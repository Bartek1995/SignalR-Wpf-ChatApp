using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views;

public partial class rate : UserControl
{
    public rate()
    {
        InitializeComponent();
        DataContext = new RateViewModel();
    }
}