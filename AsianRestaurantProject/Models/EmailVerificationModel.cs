using AsianRestaurantProject.Data;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace AsianRestaurantProject.Models
{
  public class EmailVerificationModel
  {
    public string Id { get; set; }//User email varchar256
    public byte[] RandNum { get; set; }// cryptographically secure random number 256 bits
    public double ExpTime { get; set; } //Ao date
    public byte[] IV { get; set; } //128 bits
    public byte[] Key { get; set; } //512 bits

    public void CreateKey()
    {
      byte[] idArray = Encoding.Unicode.GetBytes("gcvcuuergihu");
      byte[] expiryTimeArray = BitConverter.GetBytes(ExpTime);
      byte[] hash = SHA512.Create().ComputeHash(idArray.Concat(expiryTimeArray).Concat(RandNum).ToArray());
      Aes crypt = Aes.Create();
      crypt.Key = DataHolder.CryptoKey;
      Key = crypt.EncryptCfb(hash,IV);

      

    }
    public SqlCommand GetQuery()
    {
        SqlCommand command = new SqlCommand("USE UserData INSERT INTO OngoingEmailVerifications (@email, @AuthRNG ,@ExpTime,CryptoKey VARBINARY(512) VALUES(@Email,CONVERT(VARBINARY(256) , @AuthRng),@ExpTime,CONVERT(VARBINARY(512),@CryptoKey) ))");
            command.Parameters.Add("@email",System.Data.SqlDbType.VarChar,256,Id);
            command.Parameters.Add("@AuthRng", System.Data.SqlDbType.VarBinary, 32, Encoding.Default.GetString(RandNum));
            command.Parameters.Add("@ExpTime", System.Data.SqlDbType.VarBinary, 64, Encoding.Default.GetString(BitConverter.GetBytes(BitConverter.DoubleToInt64Bits(ExpTime))));
            command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 256, Id);
      return command;
        }
  }
}
