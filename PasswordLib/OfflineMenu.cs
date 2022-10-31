namespace PasswordGenerator;

public class OfflineMenu
{

    private static string username;
    
    public static void Offline()
    {
        string answer;

        MainMenu.Online = false;
        Console.WriteLine("What is your online username ?");
        username = Console.ReadLine();
        
        Console.WriteLine("Welcome " + username + " you are now in the Offline Mode! ");
                
        do
        {
            Console.WriteLine("Choose an option 1: create password 2: check passwords 3:CLose");
            answer = Console.ReadLine();
            if (answer == "1")
            {
                passwordAndFileMethod.FirstGeneratePassword();
            }
            else if (answer == "2")
            {
                string check;
                do
                {
                    Console.WriteLine("1 : List Password associate with site, 2 : see all your passwords, 3 : quit");
                    check = Console.ReadLine();
                    if (check == "1")
                    {
                        List<Password> passwords = passwordAndFileMethod.OpenFile();
                        passwordAndFileMethod.ListPasswordBySite(passwords);
                    }
                    else if (check == "2")
                    {
                        List<Password> passwords = passwordAndFileMethod.OpenFile();
                        passwordAndFileMethod.ListPassword(passwords);
                        check = "3";
                    }
                } while (check != "3");
            }

            if (answer == "3")
            {
                Console.Clear();
                Console.WriteLine("GoodBye " + username);
            }
        } while (answer != "3");
        
    }
    
    public static string GetUserOffline()
    {
        return username;
    }
}