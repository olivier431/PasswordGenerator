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

        static async Task Main(string[] args)
        {
            string site = "";
            string userName = "";
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

            string password = Generator(length, lower, hupper, number, symbol);

            Console.WriteLine(password);
            
            List<Password> passwords = new List<Password>()
            {
                new(password, "test", userName, site)
               
            };

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(passwords, options);
            Console.WriteLine(json);

            passwords = JsonConvert.DeserializeObject<List<Password>>(json) ?? new List<Password>();
            Console.WriteLine(string.Join("\n", passwords));

        }
        
        
    }
}


