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
            int find = 0;
            foreach (var pswdoOn in CurrentPasswordsOnline)
            {
                if ((pswdOff.Site == pswdoOn.site) && (pswdOff.UserName == pswdoOn.login) && (pswdoOn.user_id == CurrentUser.id))
                {
                    
                    if (pswdOff.ModifiedAt > pswdoOn.ModifiedAt)
                    {
                        DB.UpdatePassword(pswdOff.id, CurrentUser.id, pswdOff.Site, pswdOff.UserName, pswdOff.Encrypted);
                    }
                    else if (pswdOff.ModifiedAt < pswdoOn.ModifiedAt)
                    {
                        pswdOff.Encrypted = pswdoOn.Password;
                        pswdOff.Site = pswdoOn.site;
                        pswdOff.UserName = pswdoOn.login;
                    }

                    find++;
                }   

            }

            if (find != 1)
            {
                DB.AddPassword(CurrentUser.id, pswdOff.Site, pswdOff.UserName, pswdOff.Encrypted);
            }
        }

        foreach (var pswdOn in CurrentPasswordsOnline)
        {
            int find = 0;
            foreach (var pswdOff in CurrentPasswordsOffline)
            {
                if ((pswdOff.Site == pswdOn.site) && (pswdOff.UserName == pswdOn.login))
                {
                    find++;
                }
                
            }
            if (find != 1)
            {
                passwordAndFileMethod.AddPassword(pswdOn.Password, CurrentUser.password, pswdOn.login, pswdOn.CreatedAt, pswdOn.ModifiedAt, pswdOn.site);
            }
        }
    }

}
