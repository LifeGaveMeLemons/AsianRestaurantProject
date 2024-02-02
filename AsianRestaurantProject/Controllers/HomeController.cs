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

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
            ViewData["LoggedOn"] = "user";
			return View(new List<DataElementModel>() { new DataElementModel("r", "r", 4f, 1) });
        }
		private static string IsAuthenticated(IRequestCookieCollection cookies)
		{
			return "t";
			SQLConn
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