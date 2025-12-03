namespace EventCalendar.Classes;

public class DateEvent(
    string title,
    DateTime date,
    string description,
    List<string> invitees,
    DateEvent.EventStatus status)
{
    public DateTime Date { get; set; } = date;
    public string EventTitle { get; set; } = title;
    public string EventDescription { get; set; } = description;

    public enum EventStatus
    {
        Upcoming,
        Ongoing,
        Completed
    }

    public List<string> Invitees { get; set; } = invitees;
    public EventStatus Status { get; set; } = status;
}