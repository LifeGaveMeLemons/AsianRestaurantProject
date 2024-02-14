namespace AsianRestaurantProject.Models
{
  public class EmailVerificationModel
  {
    public string Id { get; set; }//User email varchar256
    public string AuthID { get; set; }//authenticationId to make a composite key int32
    public string RandNum { get; set; }// cryptographically secure random number 256 bits
    public double ExpTime { get; set; } //Ao date
    public string Key { get; set; } //512 bits
  }
}
