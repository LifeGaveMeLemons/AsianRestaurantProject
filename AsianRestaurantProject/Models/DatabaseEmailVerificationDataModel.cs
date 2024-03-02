namespace AsianRestaurantProject.Models
{
	public class DatabaseEmailVerificationDataModel
	{
		string Email { get; set; }
		string RandNum { get; set; }
		string Expdate { get; set; }
		string Key { get; set; }

		public DatabaseEmailVerificationDataModel(string email)
		{

		}
	}
}
