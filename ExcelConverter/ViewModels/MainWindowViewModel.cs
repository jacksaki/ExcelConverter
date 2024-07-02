using ExcelConverter.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelConverter.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public AppConfig Config { get; }
        public MainWindowViewModel(AppConfig config) : base()
        {
            this.Config = config;
            this.ThemeBoxViewModel = new ThemeBoxViewModel(config.ThemeConfig);
            this.HomeBoxViewModel = new HomeBoxViewModel(this);
        }

        public ThemeBoxViewModel ThemeBoxViewModel { get; }
        public HomeBoxViewModel HomeBoxViewModel { get; }
    }
}
