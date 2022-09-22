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

    public static void ShowDecryptPassword(Password password)
    {
        Console.WriteLine("What is your MasterKey ?");
        string key = Console.ReadLine();
        password.Decrypt(key);
        Console.WriteLine(" Password: " + password.Plaintext);
        password.Encrypt(key);
    }
    
    public static void EditList(Password password)
    {
        Console.WriteLine("do you want update 1 : username, 2 : password, 3 : site");
        string check2 = Console.ReadLine();
        if (check2 == "1")
        {
            Console.WriteLine("enter your new username");
            password.UserName = Console.ReadLine();

        }
        else if(check2 == "2")
        {
            Console.WriteLine("What is your MasterKey ?");
            string key = Console.ReadLine();
            password.Decrypt(key);
            Console.WriteLine("Enter your new password ");
            password.Plaintext = Console.ReadLine();
            password.Encrypt(key);
        }
        else if (check2 == "3")
        {
            Console.WriteLine("enter your new site");
            password.Site = Console.ReadLine();
        }

    }

    public static void Hide(Password password)
    {
        Console.Clear();
        Console.WriteLine("Username: " + password.UserName + " Password: The password is hidden");
    }
    
    public static void Delete(List<Password> passwords, Password password)
    {
        passwords.Remove(password);
        Console.WriteLine("Your Password is now delete");
    }

}
