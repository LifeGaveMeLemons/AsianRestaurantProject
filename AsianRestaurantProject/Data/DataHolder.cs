using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace AsianRestaurantProject.Data
{
  public static class DataHolder
  {
    public static byte[] CryptoKey { get { return key; } }
    private static byte[] key;
    public static void SetCryptoKey(string v)
    {
      SHA512 SHA512 = SHA512.Create();
      key = SHA512.ComputeHash(Encoding.Unicode.GetBytes(v));
    }
    
  }
}
