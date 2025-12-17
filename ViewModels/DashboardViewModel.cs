using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using EventCalendar.Classes;
using EventCalendar.Interfaces;
using EventCalendar.Models;

namespace EventCalendar.ViewModels;

public partial class DashboardViewModel: ViewModelBase
{

    [ObservableProperty] private string _welcomeMessage = "Welcome!";
    [ObservableProperty] private ObservableCollection<DateEvent> _upcomingEvents;
    
    
    public DashboardViewModel()
    {
        WelcomeMessage = $"Welcome, {App.Current.Properties["Username"].ToString().Split(' ')[0]}!";
        UpcomingEvents = new ObservableCollection<DateEvent>()
        {
            new DateEvent("Team Meeting", DateTime.Now.AddDays(2).AddHours(3),
                "Discuss project updates and next steps.", new List<string>(), DateEvent.EventStatus.Ongoing),
            new DateEvent("Doctor Appointment", DateTime.Now.AddDays(2).AddHours(1), "Annual check-up with Dr. Smith.",
                new List<string>(), DateEvent.EventStatus.Ongoing),
            new DateEvent("Birthday Party", DateTime.Now.AddDays(7).AddHours(5), "Celebrate at the local restaurant.",
                new List<string>(), DateEvent.EventStatus.Ongoing)
        };

    }

}