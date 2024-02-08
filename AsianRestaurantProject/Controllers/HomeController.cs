using AsianRestaurantProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Authentication;
using Microsoft.Data.SqlClient;
using NETCore.MailKit;
using System.Security.Cryptography;
using System.Text.Unicode;
using System.Text;

namespace AsianRestaurantProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=UserData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public IActionResult GetCreationCredentials(AccountCreationModel credentials)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				using (SqlCommand command = new SqlCommand("USE UserData INSERT INTO users (users.id,users.password,users.forename,users.lastname) VALUES (@email,@password,@name,@lastName)", conn))
				{
					byte[] salt = new byte[256];
					RandomNumberGenerator rng = RandomNumberGenerator.Create();
					rng.GetBytes(salt);
					SHA256 hash = SHA256.Create();
					byte[] arr = Encoding.Unicode.GetBytes(credentials.Password);
					
					command.Parameters.AddWithValue("@email", credentials.Email);

					command.Parameters.AddWithValue("@password", credentials.Password);
					command.Parameters.AddWithValue("@name", credentials.Forename);
					command.Parameters.AddWithValue("@lastName", credentials.Lastname);
					if (command.ExecuteNonQuery() == 0)
					{
						conn.Close();
						return Content("no entry");
					}
					conn.Close();
				}
			}
			return Content("200");

		}
		public IActionResult AccountCreation()
		{
			return View();
		}
		public IActionResult Index()
		{
			Console.WriteLine("rkfjfiuherfuiyeguyrfgwueyfgwe7y");
			ViewData["LoggedOn"] = "admin";
			return View(new List<DataElementModel>() { new DataElementModel("r", "r", 4f, 1) });
        }
		private static string IsAuthenticated(IRequestCookieCollection cookies)
		{
			return "t";
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				using (SqlCommand query = new SqlCommand("SELECT forename,lastname FROM\r\n (SELECT id FROM authentications WHERE id = @username AND authRng = @authRng) AS authMatches INNER JOIN users ON authMatches.id = users.id\r\n\r\n\r\n", conn))
				{
					query.Parameters.Add("username", System.Data.SqlDbType.VarChar, 256).Value = //CHANGE HERE;
					query.Parameters.Add("authRng", System.Data.SqlDbType.Int).Value = 1;//CHANGE HERE
				}

			}
			//Check: IP RandNumber user ExpirationDate 
			if (cookies["auth"] == "true")
			{
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}