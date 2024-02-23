using AsianRestaurantProject.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace AsianRestaurantProject.Models
{
    public class EmailVerificationModelIntermediate
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

       public EmailVerificationModel Normalize()
        {
            EmailVerificationModel model = new EmailVerificationModel();
            model.ExpTime = this.ExpTime;
            model.Id = this.Id;
            model.IV = Encoding.Unicode.GetBytes(this.IV);
            model.Key = Encoding.Unicode.GetBytes(this.Key);
            model.RandNum = Encoding.Unicode.GetBytes(this.RandNum);
            return model;
        }
    }
}
