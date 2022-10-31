using System.ComponentModel;
using MySql.Data.MySqlClient;

namespace PasswordGenerator;

public class Sync
{
    private static DB_Methode DB = PasswordUserDB_Method.GetDB();
    private static UserDB CurrentUser = PasswordUserDB_Method.GetUser();
    public static void Synchronisation()
    {
        List<Password> CurrentPasswordsOffline = new List<Password>();
        CurrentPasswordsOffline = passwordAndFileMethod.OpenFile();
        
        List<PasswordDB> CurrentPasswordsOnline = new List<PasswordDB>();

        CurrentPasswordsOnline = DB.GetUserPasswords(CurrentUser.id);

        foreach (var pswdOff in CurrentPasswordsOffline)
        {
            if (pswdOff.id == 0)
            {
                DB.AddPassword(CurrentUser.id, pswdOff.Site, pswdOff.UserName, pswdOff.Encrypted);
                
                
                CurrentPasswordsOnline = DB.GetUserPasswords(CurrentUser.id);
                pswdOff.id = CurrentPasswordsOnline.Last().id;
            }

            else
            {
                var db = CurrentPasswordsOnline.Find(x => x.id == pswdOff.id);

                if (pswdOff.ModifiedAt > db.ModifiedAt)
                {
                    DB.UpdatePassword(pswdOff.id,CurrentUser.id, pswdOff.Site, pswdOff.UserName, pswdOff.Encrypted);
                }
            }
            
        }
 
        
        
        

    }

}
