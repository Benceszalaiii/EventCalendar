using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventCalendar.Views.Pages;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace EventCalendar.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationTheme _currentTheme;

    [ObservableProperty] 
    private string _currentThemeText = "System";
    
    [ObservableProperty]
    private ICollection<object> _menuItems = new ObservableCollection<object>();
    
    [RelayCommand]
    private void ToggleTheme()
    {
        // Use the generated property (CurrentTheme), not the backing field (_currentTheme)
        CurrentTheme = CurrentTheme == ApplicationTheme.Light
            ? ApplicationTheme.Dark
            : ApplicationTheme.Light;
            
        // Use the generated property (CurrentThemeText), not the backing field (currentThemeText)
        CurrentThemeText = CurrentTheme == ApplicationTheme.Dark ? "🌙 Dark" : "☀️ Light";
        
        ApplicationThemeManager.Apply(CurrentTheme);
    }
    
    public MainViewModel()
    {
        
        // Theme config
        CurrentTheme = ApplicationTheme.Dark;
        CurrentThemeText = "🌙 Dark";
        ApplicationThemeManager.Apply(CurrentTheme);
        ApplicationThemeManager.Changed += OnThemeChanged;
        
        // WPF-UI navigation
        MenuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("Home", SymbolRegular.Home24, typeof(DashboardPage)),
            new NavigationViewItem("Settings", SymbolRegular.Settings24, typeof(SettingsPage))
        };
    }
    
    private void OnThemeChanged(ApplicationTheme currentTheme, Color systemAccent)
    {
        // Use the generated properties, not the backing fields
        CurrentTheme = currentTheme;
        CurrentThemeText = currentTheme == ApplicationTheme.Dark ? "🌙 Dark" : "☀️ Light";
    }
}