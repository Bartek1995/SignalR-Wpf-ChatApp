using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views;

public partial class addFish : UserControl
{
    public addFish()
    {
        InitializeComponent();
        DataContext = new AddFishViewModel();
    }
}