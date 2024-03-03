namespace AsianRestaurantProject.Models
{
	public class DatabaseEmailVerificationDataModel
	{
		public string Email { get; set; }
		public string RandNum { get; set; }
		public double Expdate { get; set; }
		public string Key { get; set; }

		public DatabaseEmailVerificationDataModel(string email,string randNum, double expdate, string key)
		{
			Email = email;
			RandNum = randNum;
			Expdate = expdate;
			Key = key;
		}
	}
}
