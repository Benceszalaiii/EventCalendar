using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventCalendar.Models.Main;

public class User
{
    public string Name;
    public string Image;

    public User(MainModel mainModel)
    {
        Dictionary<string, string>? data = FetchData(mainModel.Client, mainModel.DeviceId, mainModel.Token);
        if (data == null)
        {
            throw new Exception("Failed to fetch user data. Server may be unreachable.");
        }

        if (!data.ContainsKey("Name") || !data.ContainsKey("Image"))
        {
            Console.WriteLine($"Received data: {JsonSerializer.Serialize(data)}");
            throw new Exception("Invalid user data received.");
        }

        Name = data["Name"];
        Image = data["Image"];
        Console.WriteLine($"Name: {Name}, Image: {Image}");
    }

    private static Dictionary<string, string>? FetchData(HttpClient client, string deviceId, string token)
    {
        client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {token}");
        Dictionary<string, string>? data = client.GetAsync($"/authentication/user?id={deviceId}").Result.Content
            .ReadFromJsonAsync<Dictionary<string, string>>().Result;
        return data;
    }
}