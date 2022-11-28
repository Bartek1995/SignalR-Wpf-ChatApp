using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class AppView : Window
    {
        public AppView(AppViewModel context)
        {
            InitializeComponent();

            DataContext = context;
        }
    }
}
