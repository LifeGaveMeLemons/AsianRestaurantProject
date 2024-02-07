using AsianRestaurantProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Authentication;
using Microsoft.Data.SqlClient;

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
		public IActionResult GetCreationCedentials(AccountCreationModel credentials)
		{
			Console.WriteLine("gvyt");
			return null;
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