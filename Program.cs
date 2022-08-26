using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace PasswordGeneratorV1
{
    class Program
    {

        private static readonly Regex regex = new Regex(@"^\d+$");
        
        static string Generator(int length)
        {
            bool Lower = true;
            bool Hupper = false;
            bool Number = true;
            bool Symbol = false;
            
            string pattern = "";

            if (Lower)
            {
                pattern += "^(?=.*[a-z])";
            }
            
            if (Hupper)
            {
                pattern += "(?=.*[A-Z])";
            }
            
            if (Number)
            {
                pattern += "(?=.*\\d)";
            }

            if (Symbol)
            {
                pattern += "(?=.*[-+_!@#$%^&*., ?()]).+$";
            }

            Regex regexValid = new Regex(pattern);

            string Lowerletter = "abcdefhijklmnopqrstuvwxyz";
            string Hupperletter = "ABCDEFHIJKLMNOPQRSTUVWXYZ";
            string Numbers = "123456789";
            string Symbols = "!@#$%?&*()";
            string range = "";
            string password = "";

            if (Lower) range += Lowerletter;
            if (Hupper) range += Hupperletter;
            if (Number) range += Numbers;
            if (Symbol) range += Symbols;

            do
            {
                password = ""; 
                for (int i = 0; i < length; i++)
                {
                    int number = RandomNumberGenerator.GetInt32(0, range.Length);
                    password += range[number];
                }

            } while (!regexValid.IsMatch(password));

            Console.WriteLine(password);
            return password;
        }
        static void Main(string[] args)
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

            try
            {
                using (FileStream fileStream = new("TestData.txt", FileMode.OpenOrCreate))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] key =
                        {
                            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                        };
                        aes.Key = key;

                        byte[] iv = aes.IV;
                        fileStream.Write(iv, 0, iv.Length);

                        using (CryptoStream cryptoStream = new(
                                   fileStream,
                                   aes.CreateEncryptor(),
                                   CryptoStreamMode.Write))
                        {
                            using (StreamWriter encryptWriter = new(cryptoStream))
                            {
                                encryptWriter.WriteLine(Generator(length));
                            }
                        }
                    }
                }

                Console.WriteLine("The file was encrypted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The encryption failed. {ex}");
            }
        }
        
        
    }
}


