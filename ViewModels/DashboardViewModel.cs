using CommunityToolkit.Mvvm.ComponentModel;
using EventCalendar.Interfaces;
using EventCalendar.Models;

namespace EventCalendar.ViewModels;

public partial class DashboardViewModel: ViewModelBase
{

    [ObservableProperty] private string _welcomeMessage = "Welcome!";
    
    
    public DashboardViewModel()
    {
        WelcomeMessage = $"Welcome, {App.Current.Properties["Username"].ToString().Split(' ')[0]}!";
    }
    
}