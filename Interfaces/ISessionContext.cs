using System.ComponentModel;
using EventCalendar.Models.Main;

namespace EventCalendar.Interfaces;

public interface ISessionContext: INotifyPropertyChanged
{
    User User { get; set; }
}