using System.ComponentModel;
using MySql.Data.MySqlClient;

namespace PasswordGenerator;

public class Sync
{
    private static DB_Methode DB = PasswordUserDB_Method.GetDB();
    private static UserDB CurrentUser = PasswordUserDB_Method.GetUser();
    public static void Synchronisation()
    {
       
        List<Password> CurrentPasswordsOffline =  passwordAndFileMethod.OpenFile();

        List<PasswordDB> CurrentPasswordsOnline = DB.GetUserPasswords(CurrentUser.id);

        foreach (var pswdOff in CurrentPasswordsOffline)
        {
            foreach (var pswdoOn in CurrentPasswordsOnline)
            {
                if ((pswdOff.Site == pswdoOn.site) && (pswdOff.UserName == pswdoOn.login) && (pswdOff.id != 0))
                {
                    Console.WriteLine("password in the DB");
                }
                else
                {
                    DB.AddPassword(CurrentUser.id, pswdOff.Site, pswdOff.UserName, pswdOff.Encrypted);
                    CurrentPasswordsOnline = DB.GetUserPasswords(CurrentUser.id);
                    pswdOff.id = CurrentPasswordsOnline.Last().id;
                }
            }
        }
    }

}
