namespace PasswordGenerator;

public class PasswordDB
{
    
    public int id { get; set; }
    public int user_id{ get; set; }
    public string site{ get; set; }
    public string login{ get; set; }
    public static string Password { get; set; }
    
    public string PasswordNoStatic { get; set; }
    public static string Encrypted { get; set; }
    
    //public DateTime CreatedAt { get; set; }
    
    //public DateTime ModifiedAt { get; set; }
    //mettre date dans le constructeur
    
    
    public PasswordDB(int id, int userId, string site, string login, string password)
    {
        this.id = id;
        user_id = userId;
        this.site = site;
        this.login = login;
        Password = password;
        //CreatedAt = createdAt;
        //ModifiedAt = modifiedAt;
        PasswordNoStatic = password;
    }
    
    public static void Encrypt(String key)
    {
        if (Password != null)
        {
            Encrypted = new AesCrypter(key).Encrypt(Password);
            Password = null;
        }
    }
    
    public void Decrypt(string key)
    {
        Password = new AesCrypter(key).Decrypt(Encrypted);
    }

}