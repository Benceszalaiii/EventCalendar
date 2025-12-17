using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCalendar.Models;

public class CreateModel
{
    public string FormEventTitle = string.Empty;
    public string FormEventDescription = string.Empty;
    public DateTime FormDate = DateTime.UtcNow;

    public CreateModel()
    {

    }
}
