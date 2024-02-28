using AsianRestaurantProject.Data;
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

    public bool CheckKey()
    {
      //Checks if recieved url's data signature is correct before commiting to IO
      byte[] concat = GetUnhashedSignature();
      Aes crypt = Aes.Create();
      crypt.Key = DataHolder.CryptoKey;
      byte[] decryptedKey = crypt.DecryptCfb(Encoding.UTF8.GetBytes(Key), Encoding.UTF8.GetBytes(IV));
      return concat.SequenceEqual(decryptedKey);
    }
    public void CreateKey()
    {
      byte[] hash = SHA512.Create().ComputeHash(GetUnhashedSignature());
      Aes crypt = Aes.Create();
      crypt.Key = DataHolder.CryptoKey;
      Key = Encoding.UTF8.GetString(crypt.EncryptCfb(hash,GetInitializationVector()));
    }
    private byte[] GetUnhashedSignature()
    {
      return Encoding.UTF8.GetBytes(Id).Concat(BitConverter.GetBytes(ExpTime)).Concat(Encoding.UTF8.GetBytes(RandNum)).ToArray();
    }
    private byte[] GetInitializationVector() 
    {
      return Encoding.UTF8.GetBytes(IV);
    }
    public SqlCommand GetQuery()
    {
        SqlCommand command = new SqlCommand("USE UserData INSERT INTO OngoingEmailVerifications (@email, @AuthRNG ,@ExpTime,CryptoKey VARBINARY(512) VALUES(@Email,CONVERT(VARBINARY(256) , @AuthRng),@ExpTime,CONVERT(VARBINARY(512),@CryptoKey) ))");
            command.Parameters.Add("@email",System.Data.SqlDbType.VarChar,256,Id);
            command.Parameters.Add("@AuthRng", System.Data.SqlDbType.VarBinary, 32, RandNum);
            command.Parameters.Add("@ExpTime", System.Data.SqlDbType.VarBinary, 64, Encoding.Default.GetString(BitConverter.GetBytes(BitConverter.DoubleToInt64Bits(ExpTime))));
            command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 256, Id);
        return command;
        }
    public EmailVerificationModel(string email)
    {
      //Set User email
      Id = email;
      //Create RNG for use
      RandomNumberGenerator rng = RandomNumberGenerator.Create();
      //Generate random number
      byte[] randomNumber = new byte[32];
      rng.GetBytes(randomNumber);
      RandNum = Encoding.UTF8.GetString(randomNumber);
      //Set currebt time
      ExpTime = DateTime.Now.AddDays(1).ToOADate();
      //Generate a IV for encryption
      byte[] ivBytes = new byte[16];
      rng.GetBytes(ivBytes);
      IV = Encoding.UTF8.GetString(ivBytes);

    }
  }
}
