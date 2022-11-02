using System.Collections.Concurrent;

namespace PasswordGenerator;

public class PasswordVerification
{
    private static DB_Methode DB;
    private static UserDB CurrentUser;
    private static int length = 8;
    private static int month = 6;
    
    public static void VerificationOnline(string key)
    {
       
        DB = PasswordUserDB_Method.GetDB();
        CurrentUser = PasswordUserDB_Method.GetUser();
        

        string pswd1 = "";
        int longeurNoOk = 0;
        int moisNoOk = 0;
        List<PasswordDB> CurrentPasswordsOnline = DB.GetUserPasswords(CurrentUser.id);
        foreach (var pswd in CurrentPasswordsOnline)
        {
            pswd1 = EncryptAndDecrypt.Decrypt(key, pswd.Password);
            if (pswd1.Length < length)
            {
                Console.WriteLine("Warning! Your password is less than 8 in length : " + pswd.login + " " + pswd.site + " " + pswd.Password);
                longeurNoOk++;
            }
            int Month = pswd.CreatedAt.Month - DateTime.Now.Month;
            int Year = pswd.CreatedAt.Year - DateTime.Now.Year;
            Month += Year * 12;
            if (Month > month)
            {
                Console.WriteLine("Please note that your password is more than 6 months old : " + pswd.login + " " + pswd.site + " " + pswd.Password);
                moisNoOk = 0;
            }
        }
        if (longeurNoOk == 0 && moisNoOk == 0)
        {
            Console.WriteLine("nothing to report !");
        }

    }
    
    public static void VerificationOffline(string key)
    {

        string pswd1 = "";
        int longeurNoOk = 0;
        int moisNoOk = 0;
        List<Password> CurrentPasswordsOffline = passwordAndFileMethod.OpenFile();
        foreach (var pswd in CurrentPasswordsOffline)
        {
            pswd1 = EncryptAndDecrypt.Decrypt(key, pswd.Encrypted);
            if (pswd1.Length < length)
            {
                Console.WriteLine("Warning! Your password is less than 8 in length : " + pswd.UserName + " " + pswd.Site + " " + pswd.Encrypted);
                longeurNoOk++;
            }
            int Month = pswd.CreatedAt.Month - DateTime.Now.Month;
            int Year = pswd.CreatedAt.Year - DateTime.Now.Year;
            Month += Year * 12;
            if (Month > month)
            {
                Console.WriteLine("Please note that your password is more than 6 months old : " + pswd.UserName + " " + pswd.Site + " " + pswd.Encrypted);
                moisNoOk++;
            }
        }

        if (longeurNoOk == 0 && moisNoOk == 0)
        {
            Console.WriteLine("nothing to report !");
        }
    }
}