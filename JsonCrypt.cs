namespace PasswordGenerator;

public class Password
{
    public string UserName { get; set; }
    // [JsonIgnore]
    public string? Plaintext { get; set; }
    public string Encrypted { get; set; }

    public Password(string userName, string plaintext)
    {
        UserName = userName;
        Plaintext = plaintext;
        Encrypt();
    }

    public void Encrypt()
    {
        //TODO: encrypt the Plaintext password and put it in Encrypted
        Encrypted = "Encrypted";
    }
    
    public void Decrypt()
    {
        //TODO: decrypt the Encrypted password and put it in Plaintext
    }

    public override string ToString()
    {
        return $"{UserName} --> {Plaintext ?? "<null>"} --> {Encrypted}";
    }
}