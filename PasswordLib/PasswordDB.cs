namespace PasswordGenerator;

public class PasswordDB
{
    
    public int id { get; set; }
    public int user_id{ get; set; }
    public string site{ get; set; }
    public string login{ get; set; }
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
    
    public PasswordDB(int id, int userId, string site, string login, string password, DateTime createdAt, DateTime modifiedAt)
    {
        this.id = id;
        user_id = userId;
        this.site = site;
        this.login = login;
        Password = password;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
    }
    
    
}