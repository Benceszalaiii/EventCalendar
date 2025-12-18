using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
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

    private MainModel MainModel;
    
    public DashboardViewModel()
    {
        WelcomeMessage = $"Welcome, {App.Current.Properties["Username"].ToString().Split(' ')[0]}!";
        MainModel = new MainModel();
        UpcomingEvents = new ObservableCollection<DateEvent>();
        FetchEvents();
    }
    private class RawEventData
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
    }

    private class RawEventResponse
    {
        public List<RawEventData> events { get; set; }
    }

    public void FetchEvents()
    {
        RawEventResponse? rawEventResponse = MainModel.Client.GetFromJsonAsync<RawEventResponse>("/api/events").Result;
        if (rawEventResponse == null)
        {
            return;
        }
        foreach (RawEventData rawEvent in rawEventResponse.events) {
            UpcomingEvents.Add(new DateEvent(rawEvent.title, rawEvent.date, rawEvent.description));
        }
        
    }

}