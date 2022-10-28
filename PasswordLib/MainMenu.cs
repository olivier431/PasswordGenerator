namespace PasswordGenerator;

public class MainMenu
{
    public static bool Online { get; set; }
     
    public static void Menu()
    {
        
        Console.WriteLine("what mode do you want to be in? 1: Online or 2: Offline ? ");
        string choice = Console.ReadLine();
        if (choice == "1")
        {
            OnlineMenu.Online();
        }
        else if (choice == "2")
        {
            OfflineMenu.Offline();
            Online = false;
        }
        
    }
    
    public static bool GetStatus()
    {
        return Online;
    }
}