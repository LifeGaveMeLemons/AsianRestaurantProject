using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AsianRestaurantProject.Models
{
    public class AuthenticationJsonModel
    {
        int userId;
        int rng;
        string gmail;
        string password;

        public AuthenticationJsonModel(string inputCookie)
        {

        }
//        {
//userId:int;
//authId:int,
//dateOfAuthentication string (33)
//ipAddress:string (39)
//}
}
}
