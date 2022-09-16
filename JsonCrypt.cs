namespace PasswordGenerator;

public class Password
{
    public string UserName { get; set; }
    // [JsonIgnore]
    public string? Plaintext { get; set; }
    public string Encrypted { get; set; }
    public string? Site { get; set; }

    public Password(string plaintext, string key, string userName, string? site = null)
    {
        Site = site;
        UserName = userName;
        Plaintext = plaintext;
        
        Encrypt(key);
    }

    public void Encrypt(String key)
    {
        //TODO: encrypt the Plaintext password and put it in Encrypted
        if (Plaintext != null)
        {
            Encrypted = new AesCrypter(key).Encrypt(Plaintext);
            Plaintext = null;
        }
    }
    
    public void Decrypt(string key)
    {
        //TODO: decrypt the Encrypted password and put it in Plaintext
        Plaintext = new AesCrypter(key).Decrypt(Encrypted);
    }

    public override string ToString()
    {
        return $"{Site} --> {UserName} --> {Plaintext ?? "<null>"} --> {Encrypted}";
    }
}