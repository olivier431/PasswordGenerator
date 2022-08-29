using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PasswordGenerator;

public class GeneratorGenerator
{
    public static string Generator(int length)
            {
                bool Lower = true;
                bool Hupper = true;
                bool Number = true;
                bool Symbol = true;
                
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
    
                
                return password;
            }
}