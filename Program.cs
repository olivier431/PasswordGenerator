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
        
        private static readonly Regex regex = new Regex(@"^\d+$");

        private static string answer;
        private static List<Password> passwords = new List<Password>();
        static async Task Main(string[] args)
        {
            string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
            path = Path.Combine(path, "passwords.json");

            do
            {
                Console.WriteLine("Choose an option 1: create password 2: check passwords 3:CLose");
                answer = Console.ReadLine();
                if (answer == "1")
                { 
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

                    string password = Generator(length, lower, hupper, number, symbol);

                    Console.WriteLine(password);
                    

                    if (File.Exists(path))
                    {
                        Console.WriteLine(passwords.Count);
                        passwords = passwordAndFileMethod.OpenFile();
                        passwords.Add(passwordAndFileMethod.AddPassword(password, masterPassword, userName, site));
                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        string json = JsonSerializer.Serialize(passwords, options);
                        passwordAndFileMethod.Save(json);
                    }
                    else
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                        string json = JsonSerializer.Serialize(passwords, options);
                        passwordAndFileMethod.Save(json);
                    }
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

                                            passwordAndFileMethod.EditList(password);
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
                                    
                                        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                                        string json = JsonSerializer.Serialize(passwords, options);
                                        passwordAndFileMethod.Save(json);
                                        
                                    } while (check1 != "5");
                                }
                                else if (!(password.UserName.Equals(username)))
                                {
                                    Console.WriteLine("this username does not exist !");
                                    break;
                                }
                                
                                
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


