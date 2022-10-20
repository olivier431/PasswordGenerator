using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using static PasswordGenerator.AesCrypter;
using static PasswordGenerator.GeneratorGenerator;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace PasswordGenerator
{
    class Program
    {
        private static string answer;

        static async Task Main(string[] args)
        {
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
                Console.WriteLine("Welcome " + currentUser.login + " you are now connect ! ");
                
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
                                 //passwordAndFileMethod.ListPasswordBySite(passwords);
                                 PasswordUserDB_Method.ListDBPasswordBySite();
                             }
                             else if (check == "2")
                             {
                                 // passwords = passwordAndFileMethod.OpenFile();
                                 // passwordAndFileMethod.ListPassword(passwords);
                                 PasswordUserDB_Method.ListDBPassword();
                                 check = "3";
                             }
                         } while (check != "3");
                     }
                 } while (answer != "3");
            }
           
        }

    }
}


