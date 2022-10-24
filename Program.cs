using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;


namespace PasswordGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("what mode do you want to be in? 1: Online or 2: Offline ? ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                OnlineMenu.Online();
            }
            else
            {
                Console.WriteLine("OfflineMode");
            }
        }

    }
}


