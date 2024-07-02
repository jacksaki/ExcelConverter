using ExcelConverter.Models;
using ExcelConverter.ViewModels;
using ExcelConverter.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Utf8Json;
using Utf8Json.Resolvers;

namespace ExcelConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindow.Closing += MainWindow_Closing;

            this.MainWindow = mainWindow;
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            AppConfig config;
            if (System.IO.File.Exists(AppConfig.Path))
            {
                var configJson = System.IO.File.ReadAllBytes(AppConfig.Path);
                JsonSerializer.SetDefaultResolver(StandardResolver.AllowPrivate);
                config = JsonSerializer.Deserialize<AppConfig>(configJson);
                if (config.ThemeConfig == null)
                {
                    config.InitThemeConfig();
                }
            }
            else
            {
                config = new AppConfig();
                config.InitThemeConfig();
            }
            // Register AppConfig and other services
            services.AddSingleton(config);
            services.AddSingleton(config.ThemeConfig);

            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<ThemeBoxViewModel>();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
