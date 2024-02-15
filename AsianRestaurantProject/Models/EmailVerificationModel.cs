using AsianRestaurantProject.Data;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace AsianRestaurantProject.Models
{
  public class EmailVerificationModel
  {
    public string Id { get; set; }//User email varchar256
    public string AuthID { get; set; }//authenticationId to make a composite key int32
    public string RandNum { get; set; }// cryptographically secure random number 256 bits
    public double ExpTime { get; set; } //Ao date
    public string IV { get; set; } //128 bits
    public string Key { get; set; } //512 bits

    public void CreateKey()
    {
      byte[] idArray = Encoding.Unicode.GetBytes(Id);
      byte[] expiryTimeArray = BitConverter.GetBytes(ExpTime);
      byte[] randNumArray= Encoding.Unicode.GetBytes(RandNum);
      byte[] hash = SHA512.Create().ComputeHash(idArray.Concat(expiryTimeArray).Concat(randNumArray).ToArray());
      Aes crypt = Aes.Create();
      crypt.Key = DataHolder.CryptoKey;
      crypt.EncryptCfb(hash);

      

    }
  }
}
