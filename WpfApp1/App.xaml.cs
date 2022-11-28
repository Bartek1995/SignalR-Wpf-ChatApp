using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1;

public partial class App
{
    public ServiceProvider Provider { get; private set; }
    public AppViewModel viewModel { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Provider = serviceCollection.BuildServiceProvider();

        viewModel = Provider.GetRequiredService<AppViewModel>();

        var window = Provider.GetRequiredService<AppView>();
        window.Show();
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(typeof(AppView));
        serviceCollection.AddSingleton<AppViewModel>();
    }
}