using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using EventCalendar.Models.Main;
using Microsoft.Win32;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using MessageBoxResult = System.Windows.MessageBoxResult;

namespace EventCalendar.Models;

public class MainModel
{
    private string? _token;
    public HttpClient Client;
    public User? User;
    public string DeviceId;

    public string? Token
    {
        get { return _token; }
    }

    public MainModel()
    {
        LoginHandler();
        App.Current.Properties["Username"] = User.Name;
    }


    private void LoginHandler()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("http://localhost:3000");
        DeviceId = GetDeviceId();
        _token = GetTokenFromSystem();
        if (_token != null)
        {
            try
            {
                User = new User(this);
            }
            catch (Exception e)
            {
                MessageBox.Show("No user found. Please log in.", "Login Required", System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information, MessageBoxResult.Cancel, MessageBoxOptions.ServiceNotification);
                User = null;
                Console.WriteLine($"Error fetching user data: {e.Message}");
            }
        }
        
        if (User == null)
        {
            Console.WriteLine("No valid token found. Please log in.");
            Login();
            try
            {
                User = new User(this);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching user data: {e.Message}");
            }
            User = new User(this);
        }
        
    }
    
    private string? GetTokenFromSystem()
    {
        return Registry.GetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "token", "").ToString();
    }

    private string GetDeviceId()
    {
        string deviceId;
        if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "device_id", null) == null)
        {
            deviceId = Guid.NewGuid().ToString();
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "device_id", deviceId);
        }
        else
        {
            deviceId = Registry.GetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "device_id", null).ToString();
        }

        return deviceId;
    }

    public void Login()
    {
        // Login to api server and get token
        Console.Write(DeviceId);
        Client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
        var res = Client.GetAsync($"authentication/connect/initial?device={DeviceId}").Result.Content
            .ReadFromJsonAsync<Dictionary<string, string>>().Result;
        if (res != null && res.TryGetValue("requestId", out var requestId))
        {
            Console.WriteLine($"Request ID: {requestId}");
            Process.Start(new ProcessStartInfo
            {
                FileName = $"{Client.BaseAddress.AbsoluteUri}/authentication?id={requestId}&device={DeviceId}",
                UseShellExecute = true
            });
            int counter = 100;
            while (counter > 0)
            {
                var stateRes = Client.GetAsync($"authentication/connect/state?id={requestId}&device={DeviceId}").Result
                    .Content.ReadFromJsonAsync<Dictionary<string, string>>().Result;
                if (stateRes != null && stateRes.ContainsKey("token"))
                {
                    this._token = stateRes["token"];
                    Console.WriteLine($"Token received: {this._token}");
                    SaveToken();
                    break;
                }

                Console.WriteLine($"{100 - counter}. Polling...");
                Thread.Sleep(3000);
                counter--;
            }

            Console.WriteLine("Polling ended.");
        }
    }

    private void SaveToken()
    {
        if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "token", null) == null)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "token", this._token);
        }
        else
        {
            _token = Registry.GetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "token", null).ToString();
        }
    }
}