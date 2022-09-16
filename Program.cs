using System.Text.Json;
using System.Text.RegularExpressions;
using static PasswordGenerator.AesCrypter;
using static PasswordGenerator.GeneratorGenerator;


namespace PasswordGenerator
{
    class Program
    {

        private static readonly Regex regex = new Regex(@"^\d+$");

        static async Task Main(string[] args)
        {
        
            int length = 0;
            string TempoBuffer = "";
            Console.WriteLine("Do you want the default length(16)? (Y/N)");
            string choice = Console.ReadLine();
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

            string password = Generator(length);
            
            

            //Work on Json
            /*string fileName = "Password.json";
            if (!File.Exists(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? ""))
            {
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, password); 
                await createStream.DisposeAsync();
            }
            else
            {
                using FileStream openStream = File.OpenWrite(fileName);
                await JsonSerializer.SerializeAsync(openStream, "\n" + password); 
                await openStream.DisposeAsync();
            }
            */




            //List<Password> passwords2 = JsonSerializer.Deserialize<List<Password>>(json) ?? new List<Password>();
            //Console.WriteLine(string.Join("\n", passwords2));

        }
        
        
    }
}


