using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Restaurant.ViewModels;
using Restaurant.Views;

namespace Restaurant
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.DataContext = new LoginWindowViewModel(loginWindow);
                desktop.MainWindow = loginWindow;

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}