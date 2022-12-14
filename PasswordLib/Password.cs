using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PasswordGenerator;

public class Password
{
    public string UserName { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore] public string? Plaintext;

    [System.Text.Json.Serialization.JsonIgnore] public int id { get; set; } = 0;  
      
    public string Encrypted { get; set; }
    public string? Site { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
    

    public Password(string plaintext, string key, string userName, DateTime createdAt, DateTime modifiedAt, string? site = null)
    {
        Site = site;
        UserName = userName;
        Plaintext = plaintext;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        Encrypted = EncryptAndDecrypt.Encrypt(key, plaintext);
    }
    
    public static Password Add(int id, string plaintext, string key, string userName, DateTime createdAt, DateTime modifiedAt, string? site = null)
    {
        return new Password(plaintext, key, userName, createdAt, modifiedAt, site);
    }
    
    public override string ToString()
    {
        return $"{Site} --> {UserName} --> {Plaintext ?? "<null>"} --> {Encrypted}";
    }
}