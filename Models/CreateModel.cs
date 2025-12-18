using EventCalendar.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EventCalendar.Models;

public class CreateModel
{
    public string FormEventTitle = string.Empty;
    public string FormEventDescription = string.Empty;
    public DateTime FormDate = DateTime.UtcNow;

    private MainModel main;
    public CreateModel()
    {
        main = new MainModel();
    }
    public bool uploadEvent(DateEvent dateEvent)
    {

        return main.Client.PostAsJsonAsync<DateEvent>("/api/events", dateEvent).Result.IsSuccessStatusCode;
    }
}
