using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventCalendar.Classes;
using EventCalendar.Interfaces;
using EventCalendar.Models;

namespace EventCalendar.ViewModels;

public enum EventStatus
{
    Upcoming,
    Ongoing,
    Completed
}

public partial class CreateViewModel: ViewModelBase
{

    [ObservableProperty] private string _headerMessage = "Create new event";

    private string _title;
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(nameof(Title)); HeaderMessage = $"Create {value} - {Date}"; }
    }

    private DateTime? _date;
    public DateTime? Date
    {
        get => _date;
        set { _date = value; OnPropertyChanged(nameof(Date)); HeaderMessage = $"Create {Title} - {value}"; }
    }

    private EventStatus _status;
    public EventStatus Status
    {
        get => _status;
        set { _status = value; OnPropertyChanged(nameof(Status)); }
    }

    private string _description;
    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(nameof(Description)); }
    }
    public Array StatusValues => Enum.GetValues(typeof(EventStatus));


    
    public CreateViewModel()
    {
        HeaderMessage = "Create new event";
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    [RelayCommand]
    private void SaveCommand(object Sender) {
        MessageBox.Show($"Saving {Title}...");
    }


}