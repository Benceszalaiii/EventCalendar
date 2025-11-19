using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace EventCalendar.Models;

public class MainModel
{
    private string token;

    public string Token
    {
        get { return token; }
    }

    public MainModel()
    {
        Login();
    }

    private string GetTokenFromSystem()
    {
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string appFolder = Path.Combine(filePath, "EventCalendar");
        string tokenFile = Path.Combine(appFolder, "token.dat");
        System.IO.Directory.CreateDirectory(appFolder);
        using (FileStream fs = new FileStream(tokenFile, FileMode.OpenOrCreate))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                byte[] tokenEncrypted = Encoding.Unicode.GetBytes(sr.ReadToEnd());
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(tokenEncrypted, null,
                    DataProtectionScope.CurrentUser));
            }
        }
    }

    public void Login()
    {
        string deviceId;
        // Login to api server and get token
        if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "device_id", null) == null)
        {
            deviceId = Guid.NewGuid().ToString();
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "device_id", deviceId);
        }
        else
        {
            deviceId = Registry.GetValue(@"HKEY_CURRENT_USER\Software\EventCalendar", "device_id", null).ToString();
        }

        Console.Write(deviceId);
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:3000");
        var res = client.GetAsync($"authentication/connect/initial?device={deviceId}").Result.Content
            .ReadFromJsonAsync<Dictionary<string, string>>().Result;
        if (res != null && res.TryGetValue("requestId", out var requestId))
        {
            Console.WriteLine($"Request ID: {requestId}");
            Process.Start(new ProcessStartInfo
            {
                FileName = $"{client.BaseAddress.AbsoluteUri}/authentication?id={requestId}&device={deviceId}",
                UseShellExecute = true
            });

            int counter = 100; 
            while (counter > 0)
            {
                var stateRes = client.GetAsync($"authentication/connect/state?id={requestId}&device={deviceId}")
                    .Result.Content.ReadFromJsonAsync<Dictionary<string, string>>().Result;
                if (stateRes != null && stateRes.ContainsKey("token"))
                {
                    
                    this.token = stateRes["token"];
                    Console.WriteLine($"Token received: {this.token}");
                    SaveToken();
                    break;
                }
                else
                {
                    Console.WriteLine($"{100-counter}. Polling...");
                }

                Thread.Sleep(3000);
                counter--;
            }
            Console.WriteLine("Polling ended.");
        }
    }

    private void SaveToken()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(filePath, "EventCalendar");
            string tokenFile = Path.Combine(appFolder, "token.dat");
            using (FileStream fs = new FileStream(tokenFile, FileMode.Open))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    byte[] tokenEncrypted = ProtectedData.Protect(Encoding.Unicode.GetBytes(this.token), null,
                        DataProtectionScope.CurrentUser);
                    sw.Write(Convert.ToBase64String(tokenEncrypted));
                }
            }
        }

        private string RetrieveToken()
        {
            // Retrieve from api server
            return "";
        }
    }