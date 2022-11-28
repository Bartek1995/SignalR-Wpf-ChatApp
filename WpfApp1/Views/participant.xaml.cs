using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views;

public partial class participant : UserControl
{
    public participant()
    {
        InitializeComponent();
        DataContext = new ParticipantViewModels();
    }
}