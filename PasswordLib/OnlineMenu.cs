namespace PasswordGenerator;

public class OnlineMenu
{
    public static void Online()
    {
        string answer;
        string CheckInscription = "";
        UserDB currentUser = PasswordUserDB_Method.GetUser();
            
        if (currentUser.id == -1)
        {
            do
            {
                Console.WriteLine("Choose an option 1: Inscription 2: Connection");
                CheckInscription = Console.ReadLine();
                if (CheckInscription == "1")
                {
                    PasswordUserDB_Method.Inscription();
                }

                if (CheckInscription == "2")
                {
                    currentUser = PasswordUserDB_Method.Connection();
                    if (currentUser.id == -1)
                    {
                        Console.WriteLine("invalid password or username ! ");
                    }
                        
                }
            } while (currentUser.id == -1);
        }

        if (currentUser.id != -1)
        {
            Console.WriteLine("Welcome " + currentUser.login + " you are now connect in the Online Mode ! ");
                
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
                            PasswordUserDB_Method.ListDBPasswordBySite();
                        }
                        else if (check == "2")
                        {
                            PasswordUserDB_Method.ListDBPassword();
                            check = "3";
                        }
                    } while (check != "3");
                }

                if (answer == "3")
                {
                    Console.Clear();
                    Console.WriteLine("GoodBye " + currentUser.login);
                }
            } while (answer != "3");
        }
    }
}