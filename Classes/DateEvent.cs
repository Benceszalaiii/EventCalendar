namespace EventCalendar.Classes;

public class DateEvent(
    string title,
    DateTime date,
    string description)
{
    public DateTime Date { get; set; } = date;
    public string EventTitle { get; set; } = title;
    public string EventDescription { get; set; } = description;

    public string TitleBar { get; set; } = $"{date.ToString("D")} - {title}";


}