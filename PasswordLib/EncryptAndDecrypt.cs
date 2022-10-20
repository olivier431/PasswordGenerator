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
    
    public static string Decrypt(string key, string Encrypted)
    {
        return new AesCrypter(key).Decrypt(Encrypted);
    }
}