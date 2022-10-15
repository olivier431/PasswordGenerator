﻿namespace PasswordGenerator;

public class PasswordUserDB_Method
{
    static DB_Methode DB = new DB_Methode();
    static UserDB currentUser = new UserDB(-1);
    public static UserDB Connection()
    {
        Console.WriteLine("What is your username?");
        string username = Console.ReadLine();
        Console.WriteLine("What is your password?");
        string password = Console.ReadLine();

        return currentUser = DB.ConnecteUser(username, password);
        
    }

    public static void Inscription()
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

    public static UserDB GetUser()
    {
        return currentUser;
    }

    public static DB_Methode GetDB()
    {
        return DB;
    }

    public static void ListDBPassword()
    {
        int count = 0;
        List<PasswordDB> passwordsDB = DB.GetUserPasswords(currentUser.id);
        foreach (var password in passwordsDB)
        {
            count++;
            Console.WriteLine("Password # " + count + " Username: " + password.login + " Site: " + password.site + " Password: " + password.Password);
        }
    }

    public static void ListDBPasswordBySite()
    {
        List<PasswordDB> passwordsDB = DB.GetUserPasswords(currentUser.id);
        Console.WriteLine("type the site associated with your password");
        string site = Console.ReadLine();
        bool find = false;
        int count = 0;
        
        foreach (var password in passwordsDB.ToList())
        {
            if (password.site.Equals(site))
            {
                count++;
                find = true;
                Console.WriteLine("Password # " + count +" Username: " + password.login + " Site: " + password.site +  " Password: " + password.Password);
                string check1;
                do
                {
                    Console.WriteLine("do you want 1: decrypt, 2 : update, 3 : hide the password decrypte 4 : delete, 5 :  quit ");
                    check1 = Console.ReadLine();
                    if (check1 == "1")
                    {
                        //passwordAndFileMethod.ShowDecryptPassword(password);
                                            
                    }
                    else if (check1 == "2")
                    {

                        //passwordAndFileMethod.EditList(password, passwords);
                        check1 = "5";
                    }

                    else if (check1 == "3")
                    {
                        //passwordAndFileMethod.Hide(password);
                        check1 = "5";
                    }
                                        
                    else if (check1 == "4")
                    {
                        //passwordAndFileMethod.Delete(passwords, password);
                        check1 = "5";
                    }
                    //passwordAndFileMethod.Save(passwords);
                                        
                } while (check1 != "5");
            }
        }
        if(find == false)
        {
            Console.WriteLine("this username does not exist !");
            
        }
    }
}