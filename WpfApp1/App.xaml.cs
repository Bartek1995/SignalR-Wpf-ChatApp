using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Services;
using WpfApp1.ViewModels;
using WpfApp1.Views;

namespace WpfApp1
{
    public partial class App : Application
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
            serviceCollection.AddSingleton<IApiService, ApiService>();
        }
    }
}
