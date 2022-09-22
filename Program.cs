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
                        Console.WriteLine("1 : Password associate with your username, 2 : see the list, 3 : quit");
                        check = Console.ReadLine();
                        if (check == "1")
                        {
                            
                            Console.WriteLine("type the username associated with your password");
                            string username = Console.ReadLine();
                            passwords = passwordAndFileMethod.OpenFile();
                            foreach (Password password in passwords.ToList())
                            {
                                if (password.UserName.Equals(username))
                                {
                                    Console.WriteLine("Username: " + password.UserName + " Password: " + password.Encrypted);
                                    Console.WriteLine("do you want 1: decrypt, 2 : update, 3 : hide the password decrypte 4 : delete, 5 :  quit ");
                                    string check1 = Console.ReadLine();
                                    do
                                    {
                                        if (check1 == "1")
                                        {
                                            passwordAndFileMethod.ShowDecryptPassword(password);
                                            check1 = "5";
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
                                /*else 
                                {
                                    Console.WriteLine("this username does not exist !");
                                    break;
                                }*/
                                
                                
                            }
                        }
                        else if (check == "2")
                        {
                            passwords = passwordAndFileMethod.OpenFile();
                            foreach (Password password in passwords)
                            {
                                passwordAndFileMethod.ListPassword(password);
                            }

                            check = "3";
                        }
                    } while (check != "3");
                }
            } while (answer != "3");
        }
        
        
    }

   
}


