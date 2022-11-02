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
            Console.WriteLine("Choose an option 1: create password 2: list passwords 3:CLose");
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
                    Console.WriteLine("1 : List Password associate with site, 2 : see all your passwords, 3 : check all your passwords 4 : quit");
                    check = Console.ReadLine();
                    if (check == "1")
                    {
                        List<Password> passwords = passwordAndFileMethod.OpenFile();
                        passwordAndFileMethod.ListPasswordBySite(passwords);
                        check = "4";
                    }
                    else if (check == "2")
                    {
                        List<Password> passwords = passwordAndFileMethod.OpenFile();
                        passwordAndFileMethod.ListPassword(passwords);
                        check = "4";
                    }
                    else if (check == "3")
                    {
                        Console.WriteLine("Enter your account password. Your passwords will be decrypt during the verification");
                        string pwd = Console.ReadLine();
                        string key = HASH.Sha256(pwd);
                        PasswordVerification.VerificationOffline(key);
                        check = "4";
                    }
                } while (check != "4");
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