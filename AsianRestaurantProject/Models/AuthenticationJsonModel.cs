using System.Net;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AsianRestaurantProject.Models
{
  public class AuthenticationJsonModel
  {
    public string Id { get; set; }//User email varchar256
    public string AuthID { get; set; }//authenticationId to make a composite key int32
    public string RandNum { get; set; }// cryptographically secure random number 256 bits
    public string SrcIp { get; set; } // 128 characters
    public double ExpTime { get; set; } //Ao date
    public string Key { get; set; } //512 bits

    }
}
