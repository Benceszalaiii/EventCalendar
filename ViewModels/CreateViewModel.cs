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


    private string _description;
    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(nameof(Description)); }
    }


    private CreateModel model;

    public CreateViewModel()
    {
        model = new CreateModel();
        HeaderMessage = "Create new event";
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    private RelayCommand saveCommand;
    public ICommand SaveCommand => saveCommand ??= new RelayCommand(Save);

    private void Save()
    {
        List<string> errors = new List<string>();
        if (string.IsNullOrEmpty(Title)) {
            errors.Add("Title is mandatory. ");
        }
        if (string.IsNullOrEmpty(Description)) {
            errors.Add("Description is mandatory. ");
        }
        if (!Date.HasValue)
        {
            errors.Add("Please select a date. ");
        }
        if (errors.Count != 0)
        {
            MessageBox.Show(string.Concat(errors));
            return;
        }
        if (!Date.HasValue)
        {
            MessageBox.Show("Please select a date.");
            return;
        }
        DateEvent dateEvent = new(Title, Date.Value, Description);
        bool ok = model.uploadEvent(dateEvent);
        if (ok)
        {
            MessageBox.Show($"Successfully created event {Title} on {((Date.HasValue) ? Date.Value.ToString("D"): "no date specified")}");
        }
        else
        {
            MessageBox.Show("There was an error uploading your event. Please try again and make sure the right data is present.");
        }
    }

}