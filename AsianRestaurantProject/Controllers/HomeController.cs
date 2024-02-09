using AsianRestaurantProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Authentication;
using Microsoft.Data.SqlClient;
using NETCore.MailKit;
using System.Security.Cryptography;
using System.Text.Unicode;
using System.Text;
using System.Data;
using NETCore.MailKit.Infrastructure.Internal;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;

namespace AsianRestaurantProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=UserData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		[HttpPost]
		public IActionResult GetCreationCredentials(AccountCreationModel credentials)
		{
			System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate("K:\\nb934\\AsianRestaurantProject\\Key\\mailkit-413814-f6e6698014c0.p12", "notasecret", X509KeyStorageFlags.Exportable);
			ServiceAccountCredential cred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer("noreply.experimaentalsender@gmail.com")
			{
				Scopes = new[] { "https://mail.google.com/" },
				User = "noreply.experimaentalsender@gmail.com"
            });
				MimeMessage msg = new MimeMessage();
			msg.From.Add(new MailboxAddress("c", "noreply.experimaentalsender@gmail.com"));
            msg.To.Add(new MailboxAddress("lalalei","nb934@student.aru.ac.uk"));
			msg.Body = new TextPart("test-ignore this");
			using (SmtpClient client = new SmtpClient())
			{
				client.LocalDomain = "smtp.gmail.com";
				client.Connect("smtp.gmail.com", 587);
				client.Authenticate("noreply.experimaentalsender@gmail.com", "Securepassword123007;;");
				client.Send(msg);
				client.Disconnect(true);

			}

            using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				using (SqlCommand command = new SqlCommand("USE UserData INSERT INTO users (users.gmail,users.password,users.forename,users.lastname,users.salt) VALUES (@email,CONVERT(VARBINARY(512),@password),@Name,@lastName,CONVERT(VARBINARY(256),@salt))", conn))
				{
					byte[] salt = new byte[256];
					using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
					{
						generator.GetBytes(salt);
					}
					byte[] preHash = salt.Concat(Encoding.Unicode.GetBytes(credentials.Password)).ToArray();
					byte[] hash;
					using (SHA512 hashAlgorithm = SHA512.Create())
					{
						hash = hashAlgorithm.ComputeHash(preHash);
					}

					string a = Encoding.Default.GetString(hash);

                    command.Parameters.AddWithValue("@email", credentials.Email);
					command.Parameters.AddWithValue("@password", Encoding.Default.GetString(hash));
					command.Parameters.AddWithValue("@salt", Encoding.Default.GetString(salt));
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