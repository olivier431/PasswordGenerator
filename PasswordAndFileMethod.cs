using System.Runtime.CompilerServices;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace PasswordGenerator;

public class passwordAndFileMethod : List<Password>
{
   

    public static List<Password> OpenFile()
    {
        
        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.json");
        string json = File.ReadAllText(path);

        return JsonConvert.DeserializeObject<List<Password>>(json) ?? new List<Password>();

    }

    public static void Save(string json)
    {
        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.json");
        
        File.WriteAllText(path, json);
    }

    public static Password AddPassword(string plaintext, string key, string userName, string? site = null)
    {
        return new Password(plaintext, key, userName, site);
    }

    /*public static Password Find()
    {
       
    }*/
    
}

