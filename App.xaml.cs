
using System.Windows;
using EventCalendar.ViewModels;
using EventCalendar.Views;
using EventCalendar.Views.Pages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;

namespace EventCalendar;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    private readonly IHost _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();
        base.OnStartup(e);

        var services = new ServiceCollection();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainView>();
        
        _serviceProvider = services.BuildServiceProvider();
        var mainView = _serviceProvider.GetRequiredService<MainView>();
        mainView.Show();
    }
    
    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Main window
                services.AddScoped<MainView>();
                services.AddScoped<MainViewModel>();

                // Services
                services.AddSingleton<INavigationService, NavigationService>();

                // Pages and ViewModels
                services.AddScoped<DashboardPage>();
                services.AddScoped<DashboardViewModel>();
                services.AddScoped<SettingsPage>();
                services.AddScoped<SettingsViewModel>();
            })
            .Build();
    }
}