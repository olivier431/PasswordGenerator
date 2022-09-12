using System.Text.Json;
using System.Text.RegularExpressions;
using static PasswordGenerator.AesCrypter;
using static PasswordGenerator.GeneratorGenerator;

namespace PasswordGenerator
{
    class Program
    {
        
        private static readonly Regex regex = new Regex(@"^\d+$");

        /*public static void Encrypte(int length)
        {
            AesCrypter aesCrypter = new AesCrypter("c'est ma clé");
            string path = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName ?? "";
            path = Path.Combine(path, "passwords.dat");
            string data = Generator(length);
            aesCrypter.EncryptToFile(data, path);
            string decrypted = aesCrypter.DecryptFromFile(path);

            string encrypted = aesCrypter.Encrypt(data);
            decrypted = aesCrypter.Decrypt(encrypted);
            Console.WriteLine($"{data} {encrypted} {decrypted}");
        }*/

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
            //Encrypte(length);
            
            string fileName = "Password.json";
            using FileStream createStream = File.Create(fileName); 
            await JsonSerializer.SerializeAsync(createStream, password); 
            await createStream.DisposeAsync();

            //List<Password> passwords2 = JsonSerializer.Deserialize<List<Password>>(json) ?? new List<Password>();
            //Console.WriteLine(string.Join("\n", passwords2));

        }
        
        
    }
}


