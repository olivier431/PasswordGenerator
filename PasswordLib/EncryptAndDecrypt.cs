namespace PasswordGenerator;

public class EncryptAndDecrypt
{
  
    
    public static string Encrypted { get; set; }
    
    public static string Encrypt(String key, string passwordToEncrypt)
    {
        if (passwordToEncrypt != null)
        {
            Encrypted = new AesCrypter(key).Encrypt(passwordToEncrypt);
            passwordToEncrypt = null;
        }
        return Encrypted;
    }
    
    public string Decrypt(string key)
    {
        return new AesCrypter(key).Decrypt(Encrypted);
    }
}