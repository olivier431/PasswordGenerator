﻿using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PasswordGenerator;

public class Password
{
    public string UserName { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore] public string? Plaintext;
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

        if (Plaintext != null)
        {
            Encrypted = new AesCrypter(key).Encrypt(Plaintext);
            Plaintext = null;
        }
    }
    
    public void Decrypt(string key)
    {
        Plaintext = new AesCrypter(key).Decrypt(Encrypted);
    }
    
    public override string ToString()
    {
        return $"{Site} --> {UserName} --> {Plaintext ?? "<null>"} --> {Encrypted}";
    }
}