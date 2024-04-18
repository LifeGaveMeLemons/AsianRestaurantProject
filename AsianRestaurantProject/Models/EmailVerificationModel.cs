using AsianRestaurantProject.Data;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace AsianRestaurantProject.Models
{
  public class EmailVerificationModel
  {
    [JsonProperty("id")]
    public string Id { get; set; }//User email varchar256

    [JsonProperty("randNum")]
    public string RandNum { get; set; }// cryptographically secure random number 256 bits
    [JsonProperty("expTime")]
    public double ExpTime { get; set; } //Ao date
    [JsonProperty("iv")]
    public string IV { get; set; } //128 bits
    [JsonProperty("key")]
    public string Key { get; set; } //512 bits

    public string tst { get; set; }

    public bool CheckKey()
    {
      //Checks if recieved url's data signature is correct before commiting to IO
      byte[] concat = GetUnhashedSignature();
      Aes crypt = Aes.Create();
      crypt.Key = DataHolder.CryptoKey;

      byte[] decryptedKey = crypt.DecryptCfb(DecodeStringArray(Key), GetInitializationVector());
      byte[] hash = SHA512.HashData(concat);
      return decryptedKey.SequenceEqual(hash);
    }
    public void CreateKey()
    {
      byte[] hash = SHA512.Create().ComputeHash(GetUnhashedSignature());
      Aes crypt = Aes.Create();
      crypt.Key = DataHolder.CryptoKey;
      Key = EncodeByteArray(crypt.EncryptCfb(hash, GetInitializationVector()));
    }
    private byte[] GetUnhashedSignature()
    {
      return DecodeStringArray(U8ToB64Email(Id)).Concat(BitConverter.GetBytes(ExpTime)).Concat(DecodeStringArray(RandNum)).ToArray();
    }
    private byte[] GetInitializationVector()
    {
      byte[] iv = DecodeStringArray(IV);
      return iv;
    }
    public SqlCommand GetInsertionQuery(SqlConnection conn)
    {
      SqlCommand command = new SqlCommand("MERGE INTO OngoingEmailVerifications AS target USING (SELECT @Email AS Email, CONVERT(VARBINARY(256), @AuthRng) AS AuthRNG, @ExpTime AS ExpTime, CONVERT(VARBINARY(512), @CryptoKey) AS CryptoKey) AS source\r\nON (target.Email = source.Email) WHEN MATCHED THEN    UPDATE SET target.AuthRNG = source.AuthRNG,\r\n               target.ExpTime = source.ExpTime,\r\n               target.CryptoKey = source.CryptoKey\r\nWHEN NOT MATCHED BY TARGET THEN\r\n    INSERT (Email, AuthRNG, ExpTime, CryptoKey)\r\n    VALUES (source.Email, source.AuthRNG, source.ExpTime, source.CryptoKey);", conn);
      command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 256).Value = Id;
      command.Parameters.Add("@AuthRng", System.Data.SqlDbType.VarBinary, 256).Value = Convert.FromBase64String(RandNum);
      command.Parameters.Add("@ExpTime", System.Data.SqlDbType.VarBinary, 64).Value = BitConverter.GetBytes(BitConverter.DoubleToInt64Bits(ExpTime));
      command.Parameters.Add("@CryptoKey", System.Data.SqlDbType.VarBinary, 512).Value = Convert.FromBase64String(Key);
      return command;
    }
    public EmailVerificationModel(string email)
    {
      if (email != null)
      {
        //Set User email
        Id = email;
      }
      //Create RNG for use
      RandomNumberGenerator rng = RandomNumberGenerator.Create();
      //Generate random number
      byte[] randomNumber = new byte[32];
      rng.GetBytes(randomNumber);
      RandNum = EncodeByteArray(randomNumber);
      byte[] c = DecodeStringArray(RandNum);
      //Set currebt time
      ExpTime = DateTime.Now.AddDays(1).ToOADate();
      //Generate a IV for encryption
      byte[] ivBytes = new byte[16];
      rng.GetBytes(ivBytes);
      IV = EncodeByteArray(ivBytes);
      int l = IV.Length;
      Console.OutputEncoding = Encoding.UTF8;
      Console.WriteLine(IV);
      tst = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/==";
      byte[] bytes = Convert.FromBase64String(tst);
            Console.WriteLine(  );
        }

    string EncodeByteArray(byte[] data)
    {
      return Convert.ToBase64String(data);
    }
    private byte[] DecodeStringArray(string str)
    {
      str = str.Replace(" ", "+");
      return Convert.FromBase64String(str);
    }

    string U8ToB64Email(string email)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(email));
    }
    string B64ToU8Email(string email)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(email));
    }

    public DatabaseEmailVerificationDataModel? CheckVerificationdatabase(SqlConnection conn)
    {
      using (SqlCommand command = new SqlCommand("BEGIN TRANSACTION;\r\n\r\n\r\nSELECT * FROM OngoingEmailVerifications WHERE email = @gmail;\r\n\r\n\r\nDELETE FROM OngoingEmailVerifications WHERE email = @gmail;\r\n\r\nCOMMIT TRANSACTION;\r\n", conn))
      {
        command.Parameters.Add("@gmail", System.Data.SqlDbType.VarChar, 254).Value = Id;
        using (SqlDataReader r = command.ExecuteReader())
        {
          try
          {
            r.Read();
            DatabaseEmailVerificationDataModel data = new DatabaseEmailVerificationDataModel(
            email: (string)r["Email"],
            randNum: Convert.ToBase64String((byte[])r["AuthRng"]),
            expdate: BitConverter.ToDouble((byte[])r["ExpTime"]),
            key: Convert.ToBase64String((byte[])r["CryptoKey"])
            );
            r.Close();
            return data;
          }
          catch(InvalidOperationException)
          {
            return null;
          }

        }

      }
    }
  }
}
