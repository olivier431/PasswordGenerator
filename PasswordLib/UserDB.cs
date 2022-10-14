namespace PasswordGenerator;

public class UserDB
{
    public string login { get; set; }
    public string fullname { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    
    public int id { get; set; }

    public UserDB(int id)
    {
        this.id = id;
    }

}