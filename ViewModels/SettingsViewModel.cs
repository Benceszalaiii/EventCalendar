using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Appearance;

namespace EventCalendar.ViewModels;

public partial class SettingsViewModel: ViewModelBase
{
    [ObservableProperty]
    private ApplicationTheme _currentTheme;

    [ObservableProperty] 
    private string _currentThemeText = "System";

    public SettingsViewModel()
    {
        // Theme config
        ApplicationThemeManager.ApplySystemTheme();
    }
    
    
    
    [RelayCommand]
    private void ToggleThemeHandler()
    {
        CurrentTheme = CurrentTheme == ApplicationTheme.Light
            ? ApplicationTheme.Dark
            : ApplicationTheme.Light;
        
        CurrentThemeText = CurrentTheme == ApplicationTheme.Dark ? "Dark" : "Light";
        
        ApplicationThemeManager.Apply(CurrentTheme);
    }
    
    private void OnThemeChanged(ApplicationTheme currentTheme, Color systemAccent)
    {
        CurrentTheme = currentTheme;
        CurrentThemeText = currentTheme == ApplicationTheme.Dark ? "Dark" : "Light";
    }
}