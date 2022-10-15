using MySql.Data.MySqlClient;

namespace PasswordGenerator;

public class DB_Methode
{
     private string connStr;
    private MySqlConnection conn;
    private string sql;
    

    public DB_Methode()
    { 
        connStr = "server=192.168.74.131;user=olivier;database=PasswordGenerator;password=Frederique43!;"; 
        conn = new MySqlConnection(connStr);
        conn.Open();
  
    }

    public bool AddPassword(int user_id, String site, String login , String password)
    { 
        sql = "INSERT into passwords (user_id, site, login, password) VALUES ('" + user_id + "','" + site + "','" + login + "','"+ password + "')";
        var command = new MySqlCommand(sql, conn);
        try
        {
            int result = command.ExecuteNonQuery();
            if (result != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public bool DeletePassword(int id)
    {
        sql = "DELETE FROM passwords WHERE id ='" + id + "'";
        var command = new MySqlCommand(sql, conn);
        int result = command.ExecuteNonQuery();
        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool UpdatePassword(int id, int user_id, String site, String login, String password)
    {
        sql = "UPDATE passwords SET user_id ='" + user_id + "' ,site = '" + site + "',login = '" + login + "',password = '" + password + "',modified_at = CURRENT_DATE WHERE id = '" + id + "'";
        var command = new MySqlCommand(sql, conn);
        try
        {
            int result = command.ExecuteNonQuery();
            if (result != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public bool adduser(String username, String fullname , String email, String password)
    {
        sql = "INSERT into users (username, fullname, password, email) VALUES ('" + username + "','" + fullname + "','" + password + "','" + email + "')";
        var cmd = new MySqlCommand(sql, conn);
        try
        {
            var result = cmd.ExecuteNonQuery();
            if (result != 1)
            {
                return false;
                
            }
            else
            {
                return true;
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public UserDB ConnecteUser(String login, String password)
    {
        UserDB user = new UserDB(-1);
        sql = "SELECT * FROM users WHERE username ='" + login + "' and password = '" + password + "'";
        using (var cmd = new MySqlCommand(sql, conn))
        {

            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                user.id = reader.GetInt32(0);
                user.login = reader.GetString(1);
                user.fullname = reader.GetString(2);
                user.password = reader.GetString((3));
                user.email = reader.GetString(4);
                
            }
            reader.Close();        
        }

        ConnectedUpdate(user.id);
        return user;
    }

    public List<PasswordDB> GetUserPasswords(int id)
    {
        List<PasswordDB> user_Password = new List<PasswordDB>();

        sql = "SELECT * FROM passwords WHERE user_id ='" + id + "'";
        using (var cmd = new MySqlCommand(sql, conn))
        {
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                user_Password.Add(new PasswordDB(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
            }
            reader.Close();        
        }
        return user_Password;
    }
    
    public List<PasswordDB> GetUserPasswordsBySite(int id, string site)
    {
        List<PasswordDB> user_Password = new List<PasswordDB>();

        sql = "SELECT * FROM passwords WHERE user_id ='" + id + "' and site = '" + site + "'";
        using (var cmd = new MySqlCommand(sql, conn))
        {
            var reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                user_Password.Add(new PasswordDB(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
            }
            reader.Close();        
        }
        return user_Password;
    }

    public bool ConnectedUpdate(int id)
    {
        sql = "UPDATE users SET last_connection = NOW() WHERE id ='" + id + "'";
        var cmd = new MySqlCommand(sql, conn);
        try
        {
            var result = cmd.ExecuteNonQuery();
            if (result != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}