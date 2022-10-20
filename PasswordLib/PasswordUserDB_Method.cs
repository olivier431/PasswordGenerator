using static PasswordGenerator.PasswordDB;

namespace PasswordGenerator;

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

    public static void AddPassword(int CurrentUserId, string site, string username, string password)
    {
        string key = currentUser.password;
        Console.WriteLine(password);
        password = EncryptAndDecrypt.Encrypt(key, password);
        Console.WriteLine(password);
        DB.AddPassword(CurrentUserId, site, username, password);
        

    }

    public static void ListDBPassword()
    {
        int count = 0;
        List<PasswordDB> passwordsDB = DB.GetUserPasswords(currentUser.id);
        foreach (var password in passwordsDB)
        {
            count++;
            Console.WriteLine("Password # " + count + " Username: " + password.login + " Site: " + password.site + " Password: " + password.Password);
            string check1;
            do
            {
                Console.WriteLine("do you want 1: decrypt, 2 : update, 3 : hide the password decrypte 4 : delete, 5 :  quit ");
                check1 = Console.ReadLine();
                if (check1 == "1")
                {

                    Decrypt(password);

                }
                else if (check1 == "2")
                {
                    update(password);
                    check1 = "5";
                }

                else if (check1 == "3")
                {
                    Hide(password);
                    check1 = "5";
                }
                                        
                else if (check1 == "4")
                {
                    Delete(passwordsDB, password);
                    check1 = "5";
                }
                //passwordAndFileMethod.Save(passwords);
                                        
            } while (check1 != "5");
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
                        Decrypt(password);

                    }
                    else if (check1 == "2")
                    {

                        update(password);
                        check1 = "5";
                    }

                    else if (check1 == "3")
                    {
                        Hide(password);
                        check1 = "5";
                    }
                                        
                    else if (check1 == "4")
                    {
                        Delete(passwordsDB, password);
                        check1 = "5";
                    }
                    //passwordAndFileMethod.Save(passwords);
                                        
                } while (check1 != "5");
            }
        }
        if(find == false)
        {
            Console.WriteLine("this site does not exist !");
            
        }
    }

    public static void Decrypt(PasswordDB password)
    {
        try
        {
            Console.WriteLine("Please reenter your account password");
            string pwd = Console.ReadLine();
            string key = HASH.Sha256(pwd);
            password.Password = EncryptAndDecrypt.Decrypt(key, password.Password);
            Console.WriteLine(" Password: " + password.Password);
            password.Password = EncryptAndDecrypt.Encrypt(key, password.Password);
        }
        catch (Exception e)
        {
            Console.WriteLine("this account passord does not match with your account password");
        }
    }

    public static void update(PasswordDB password)
    {
        Console.WriteLine("do you want update 1 : username, 2 : password, 3 : site");
        string check2 = Console.ReadLine();
        if (check2 == "1")
        {
            Console.WriteLine("the username you want to update : " + password.login);
            Console.WriteLine("enter your new username");
            password.login = Console.ReadLine();
            Console.WriteLine("your new username : " + password.login);
            DB.UpdatePassword(password.id, currentUser.id, password.site, password.login, password.Password);

        }
        else if (check2 == "2")
        {
            Console.WriteLine("Please reenter your account password");
            string pwd = Console.ReadLine();
            string key = HASH.Sha256(pwd);
            try
            {
                password.Password = EncryptAndDecrypt.Decrypt(key, password.Password);
                Console.WriteLine("the password you want to update : " + password.Password);
                Console.WriteLine("a new password will be generate");
                password.Password = passwordAndFileMethod.RegeneratePassword();
                Console.WriteLine("Your password has been changed to : " + password.Password);
                password.Password = EncryptAndDecrypt.Encrypt(key, password.Password);
                DB.UpdatePassword(password.id, currentUser.id, password.site, password.login, password.Password);
            }
            catch (Exception e)
            {
                Console.WriteLine("this account passord does not match with your account password");
            }

        }
        
        else if (check2 == "3")
        {
            Console.WriteLine("the site you want to update : " + password.site);
            Console.WriteLine("enter your new site");
            password.site = Console.ReadLine();
            Console.WriteLine("your new site : " + password.site );
            DB.UpdatePassword(password.id, currentUser.id, password.site, password.login, password.Password);
        }
    }
    
    public static void Hide(PasswordDB password)
    {
        Console.Clear();
        Console.WriteLine("Username: " + password.login + " Site: " + password.site + " Password: The password is hidden");
    }
    
    public static void Delete(List<PasswordDB> passwords, PasswordDB password)
    {
        passwords.Remove(password);
        DB.DeletePassword(password.id);
        Console.WriteLine("Your Password is now delete");
    }
}