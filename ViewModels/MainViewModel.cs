using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Appearance;

namespace EventCalendar.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationTheme _currentTheme;

    [ObservableProperty] 
    private string _currentThemeText = "System";
    
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
        CurrentTheme = ApplicationTheme.Dark;
        CurrentThemeText = "🌙 Dark";
        ApplicationThemeManager.Apply(CurrentTheme);
    
        // Subscribe to system theme changes
        ApplicationThemeManager.Changed += OnThemeChanged;
    }
    
    private void OnThemeChanged(ApplicationTheme currentTheme, Color systemAccent)
    {
        // Use the generated properties, not the backing fields
        CurrentTheme = currentTheme;
        CurrentThemeText = currentTheme == ApplicationTheme.Dark ? "🌙 Dark" : "☀️ Light";
    }
}