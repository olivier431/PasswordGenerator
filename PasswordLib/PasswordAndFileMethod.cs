using System.Runtime.CompilerServices;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PasswordGenerator;

public class passwordAndFileMethod
{
    private static DB_Methode DB = PasswordUserDB_Method.GetDB();
    private static UserDB CurrentUser = PasswordUserDB_Method.GetUser();
    private static readonly Regex regex = new Regex(@"^\d+$");
    public static List<Password> OpenFile()
    {

        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.json");
        string json = File.ReadAllText(path);

        return JsonConvert.DeserializeObject<List<Password>>(json) ?? new List<Password>();

    }

    public static void Save(List<Password> passwords)
    {
        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.json");
        
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        
        string json = JsonSerializer.Serialize(passwords, options);

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
        try
        {
            password.Decrypt(key);
            Console.WriteLine(" Password: " + password.Plaintext);
            password.Encrypt(key);
        }
        catch (Exception e)
        {
            Console.WriteLine("This MasterKey does not exist");
        }
        
    }
    
    public static void EditList(Password password, List<Password> passwords)
    {
        Console.WriteLine("do you want update 1 : username, 2 : password, 3 : site");
        string check2 = Console.ReadLine();
        if (check2 == "1")
        {
            Console.WriteLine("the username you want to update : " + password.UserName);
            Console.WriteLine("enter your new username");
            password.UserName = Console.ReadLine();
            Console.WriteLine("your new username : " + password.UserName );

        }
        else if(check2 == "2")
        {
            Console.WriteLine("What is your MasterKey ?");
            string key = Console.ReadLine();
            try
            {
                password.Decrypt(key);
                Console.WriteLine("the password you want to update : " + password.Plaintext);
                Console.WriteLine("a new password will be generate");
                password.Plaintext = RegeneratePassword(passwords);
                Console.WriteLine("Your password has been changed to : " + password.Plaintext);
                password.Encrypt(key);
            }
            catch (Exception e)
            {
                Console.WriteLine("This MasterKey does not exist");
            }
            
        }
        else if (check2 == "3")
        {
            Console.WriteLine("the site you want to update : " + password.Site);
            Console.WriteLine("enter your new site");
            password.Site = Console.ReadLine();
            Console.WriteLine("your new site : " + password.Site );
        }

    }

    public static void Hide(Password password)
    {
        Console.Clear();
        Console.WriteLine("Username: " + password.UserName + " Site: " + password.Site + " Password: The password is hidden");
    }
    
    public static void Delete(List<Password> passwords, Password password)
    {
        passwords.Remove(password);
        Console.WriteLine("Your Password is now delete");
    }
    
    public static void ListPassword(List<Password> passwords)
    {
        int count = 0;
        foreach (Password password in passwords)
        {
            count++;
            
            Console.WriteLine("Password # " + count + " Username: " + password.UserName + " Site: " + password.Site + " Password: " + password.Encrypted);
        }

        if (count == 0)
        {
            Console.WriteLine("The list is empty ! Create Password to show the list !");
        }
    }

    public static void ListPasswordBySite(List<Password> passwords)
    {
        Console.WriteLine("type the site associated with your password");
        string site = Console.ReadLine();
        passwords = passwordAndFileMethod.OpenFile();
        bool find = false;
        int count = 0;
        foreach (Password password in passwords.ToList())
        {
            if (password.Site.Equals(site))
            {
                count++;
                find = true;
                Console.WriteLine("Password # " + count +" Username: " + password.UserName + " Site: " + password.Site +  " Password: " + password.Encrypted);
                string check1;
                do
                {
                    Console.WriteLine("do you want 1: decrypt, 2 : update, 3 : hide the password decrypte 4 : delete, 5 :  quit ");
                    check1 = Console.ReadLine();
                    if (check1 == "1")
                    {
                        passwordAndFileMethod.ShowDecryptPassword(password);
                                            
                    }
                    else if (check1 == "2")
                    {

                        passwordAndFileMethod.EditList(password, passwords);
                        check1 = "5";
                    }

                    else if (check1 == "3")
                    {
                        passwordAndFileMethod.Hide(password);
                        check1 = "5";
                    }
                                        
                    else if (check1 == "4")
                    {
                        passwordAndFileMethod.Delete(passwords, password);
                        check1 = "5";
                    }
                    passwordAndFileMethod.Save(passwords);
                                        
                } while (check1 != "5");
            }
        }
        if(find == false)
        {
            Console.WriteLine("this username does not exist !");
        }
    }

    public static string FirstGeneratePassword(List<Password> passwords)
    {
        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.json");
        string site = "";
        string userName = "";
        string masterPassword = "";
        bool hupper, lower, number, symbol;
        Console.WriteLine("What is your Username ?");
        userName = Console.ReadLine();
                    
        Console.WriteLine("For wich site do you want to register a password ?");
        site = Console.ReadLine();
                    
        Console.WriteLine("Do you want lowerLetter ?");
        string choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            lower = true;
        }
        else
        {
            lower = false;
        }
                    
        Console.WriteLine("Do you want hupperLetter ?");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            hupper = true;
        }
        else
        {
            hupper = false;
        }
                    
        Console.WriteLine("Do you want number ?");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            number = true;
        }
        else
        {
            number = false;
        }
                    
        Console.WriteLine("Do you want symbol ?");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            symbol = true;
        }
        else
        {
            symbol = false;
        }

        if (!(lower || hupper || symbol || number))
        {
            throw new ArgumentException("At least 1 of the character types most be present !");
        }
                    
        int length = 0;
        string TempoBuffer = "";
        Console.WriteLine("Do you want the default length(16)? (Y/N)");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            length = 16;
        }
        else
        {
            do
            {
                Console.WriteLine("How many caracters do you want ? Must be between 8 to 64 ");
                TempoBuffer = Console.ReadLine();
                if (regex.IsMatch(TempoBuffer))
                {
                    length = Convert.ToInt32(TempoBuffer);
                }

            } while (!regex.IsMatch(TempoBuffer) || (length < 8 || length > 64));
        }
                    
        Console.WriteLine("What is your masterPassword ?");
        masterPassword = Console.ReadLine();

        string password = GeneratorGenerator.Generator(length, lower, hupper, number, symbol);

        DB.AddPassword(CurrentUser.id, site, userName, password);
        
        if (File.Exists(path))
        {
            passwords = passwordAndFileMethod.OpenFile();
            passwords.Add(passwordAndFileMethod.AddPassword(password, masterPassword, userName, site));
            
            passwordAndFileMethod.Save(passwords);
        }
        else
        {
            passwordAndFileMethod.Save(passwords);
        }

        return password;
    }
    
    public static string RegeneratePassword(List<Password> passwords)
    {
        string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
        path = Path.Combine(path, "passwords.json");
        bool hupper, lower, number, symbol;
        Console.WriteLine("Do you want lowerLetter ?");
        string choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            lower = true;
        }
        else
        {
            lower = false;
        }
                    
        Console.WriteLine("Do you want hupperLetter ?");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            hupper = true;
        }
        else
        {
            hupper = false;
        }
                    
        Console.WriteLine("Do you want number ?");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            number = true;
        }
        else
        {
            number = false;
        }
                    
        Console.WriteLine("Do you want symbol ?");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            symbol = true;
        }
        else
        {
            symbol = false;
        }

        if (!(lower || hupper || symbol || number))
        {
            throw new ArgumentException("At least 1 of the character types most be present !");
        }
                    
        int length = 0;
        string TempoBuffer = "";
        Console.WriteLine("Do you want the default length(16)? (Y/N)");
        choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            length = 16;
        }
        else
        {
            do
            {
                Console.WriteLine("How many caracters do you want ? Must be between 8 to 64 ");
                TempoBuffer = Console.ReadLine();
                if (regex.IsMatch(TempoBuffer))
                {
                    length = Convert.ToInt32(TempoBuffer);
                }

            } while (!regex.IsMatch(TempoBuffer) || (length < 8 || length > 64));
        }
        string password = GeneratorGenerator.Generator(length, lower, hupper, number, symbol);
        
        passwordAndFileMethod.Save(passwords);
        
        return password;
    }
}
