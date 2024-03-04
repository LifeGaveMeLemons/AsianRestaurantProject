namespace AsianRestaurantProject.Models
{
	public class DatabaseEmailVerificationDataModel
	{
		public string Email { get; set; }
		public string RandNum { get; set; }
		public double ExpTime { get; set; }
		public string Key { get; set; }

		public DatabaseEmailVerificationDataModel(string email,string randNum, double expdate, string key)
		{
			Email = email;
			RandNum = randNum;
			ExpTime = expdate;
			Key = key;
		}
	}
}
