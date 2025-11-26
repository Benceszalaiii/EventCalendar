using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventCalendar.Models.Main;

public class User
{
    public string Name;
    public string Image;

    public User(String name, String image)
    {
        Name = name;
        Image = image;
        Console.WriteLine($"Name: {Name}, Image: {Image}");
    }

    public static User? GetUser(HttpClient client, string deviceId)
    {
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {deviceId}");
        Dictionary<string, string> data = client.GetAsync($"/authentication/user?id={deviceId}").Result.Content
            .ReadFromJsonAsync<Dictionary<string, string>>().Result;
        foreach (KeyValuePair<string, string> keyValuePair in data)
        {
            Console.WriteLine("{0}: {1}", keyValuePair.Key, keyValuePair.Value);
        }

        return null;
    }
}