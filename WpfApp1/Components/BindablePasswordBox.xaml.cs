using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Components;

/// <summary>
///     Logika interakcji dla klasy BindablePasswordBox.xaml
/// </summary>
public partial class BindablePasswordBox : UserControl
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox),
            new PropertyMetadata(string.Empty));

    public BindablePasswordBox()
    {
        InitializeComponent();
    }

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = passwordBox.Password;
    }
}