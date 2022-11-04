@startuml
class AesCrypter {
-_aes: Aes
+key: string {get,set}
+AesCrypter()
+AesCrypter(string)
+EncryptToFile(string,string)
+DecryptFromFile(string)
+Encrypt(string)
+Decrypt(string)
}

class DB_Methode{
-connStr : String
-conn : MySqlConnection
-sql : String
+DB_Methode()
+bool adduser(string,string,string,string)
+user ConnecteUser(string, string)
+List<password> GetUserPasswords(int)
+bool ConnectedUpdate(int)
+bool DeletePassword(int)
+bool AddPassword(int,string,string,string)
+bool UpdatePassword(int,int,string,string,string)   
}

class EncryptAndDecrypt{
+{static}Encrypted : String { get; set; }
+{static}String Encrypt(string,string)
+{static}String Decrypt(string,string)
}

class GeneratorGenerator{
+{static}String Generator(int,bool,bool,bool,bool)
}

class HASH{
+{static}String Sha256(string)
}

class MainMenu{
+{static}Online: bool{ get; set; }
+{static}Menu()
+{static}bool GetStatus()
}

class OfflineMenu{
+{static}username: string
+{static}Offline()
+{static}string GetUserOffline()
}

class OnlineMenu{
+{static}Online()
}

class Password{
+UserName : string { get; set; }
+Plainttext : string
+id : int { get; set; } = 0
+Encrypted: string { get; set; }
+Site: string { get; set; }
+CreatedAt: DateTime{ get; set; }
+ModifiedAt: DateTime{ get; set; }
+Password(string,string,string,DateTime,DateTime,string)
+{static}Password Add(int,string,string,string,DateTime,DateTime,string)
+ string ToString()
  }

class passwordAndFileMethod{
-{static}DB : DB_Methode
-{static}CurrentUser : UserDB
-{static}regex: Regex
-{static}username: string
+{static}List<Password> OpenFile()
+{static}Save(List<Password>)
+{static}Password AddPassword(string,string,string,DateTime,DateTime,string)
+{static}ShowDecryptPassword(Password)
+{static}EditList(Password,List<Password>)
+{static}Hide(Password)
+{static}Delete(List<Password>,Password)
+{static}ListPassword(List<Password>)
+{static}ListPasswordBySite(List<Password>)
+{static}string FirstGeneratePassword()
+{static}string RegeneratePassword()

}

@enduml