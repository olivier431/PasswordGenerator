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
        private static List<Password> passwords = new List<Password>();
        static async Task Main(string[] args)
        {
            string CheckInscription = "";
            DB_Methode DB = new DB_Methode();
            UserDB currentUser = new UserDB(-1);
            
            if (currentUser.id == -1)
            {
                do
                {
                    Console.WriteLine("Choose an option 1: Inscription 2: Connection");
                    CheckInscription = Console.ReadLine();
                    if (CheckInscription == "1")
                    {
                        Console.WriteLine("What is your name?");
                        string fullname = Console.ReadLine();
                        Console.WriteLine("What is your username?");
                        string username = Console.ReadLine();
                        Console.WriteLine("What is your password?");
                        string password = Console.ReadLine();
                        Console.WriteLine("What is your email?");
                        string email = Console.ReadLine();
                            
                        DB.adduser(username, fullname, email, password);
                    }

                    if (CheckInscription == "2")
                    {
                        Console.WriteLine("What is your username?");
                        string username = Console.ReadLine();
                        Console.WriteLine("What is your password?");
                        string password = Console.ReadLine();

                        currentUser = DB.ConnecteUser(username, password);
                        Console.WriteLine("Welcome " + currentUser.login + " you are now connect ! ");
                    }
                } while (currentUser.id == -1);
            }

            if (currentUser.id != -1)
            {
                 do
                 {
                     Console.WriteLine("Choose an option 1: create password 2: check passwords 3:CLose");
                     answer = Console.ReadLine();
                     if (answer == "1")
                     { 
                         passwordAndFileMethod.FirstGeneratePassword(passwords);
                     }
                     else if (answer == "2")
                     {
                         string check;
                         do
                         {
                             Console.WriteLine("1 : List Password associate with your username, 2 : see the list, 3 : quit");
                             check = Console.ReadLine();
                             if (check == "1")
                             {
                            
                                 Console.WriteLine("type the username associated with your password");
                                 string username = Console.ReadLine();
                                 passwords = passwordAndFileMethod.OpenFile();
                                 bool find = false;
                                 int count = 0;
                                 foreach (Password password in passwords.ToList())
                                 {
                                     if (password.UserName.Equals(username))
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
                                     break;
                                 }
                             }
                             else if (check == "2")
                             {
                                 passwords = passwordAndFileMethod.OpenFile();
                                 int count1 = 0;
                                 foreach (Password password in passwords)
                                 {
                                     count1++;
                                     passwordAndFileMethod.ListPassword(password);
                                 }

                                 if (count1 == 0)
                                 {
                                     Console.WriteLine("The list is empty ! Create Password to show the list !");
                                 }

                                 check = "3";
                             }
                         } while (check != "3");
                     }
                 } while (answer != "3");
            }
           
        }

    }
}


