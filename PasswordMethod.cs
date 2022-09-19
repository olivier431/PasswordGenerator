namespace PasswordGenerator;

public class passwordMethod
{
    public static Password AddPassword(string plaintext, string key, string userName, string? site = null)
    {
        return new Password(plaintext, key, userName, site);
    }
}

