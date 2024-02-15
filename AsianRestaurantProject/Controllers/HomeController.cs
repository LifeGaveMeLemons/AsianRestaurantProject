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
using Org.BouncyCastle.Asn1.Pkcs;
using Newtonsoft.Json;
using System.Net;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto;
using Microsoft.AspNetCore.Components.Routing;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System;

namespace AsianRestaurantProject.Controllers
{
	public class HomeController : Controller
	{
    private string CreateEmail(string Name, string LastName, string url)
    {
      return @"<!DOCTYPE html>
          < html >
          < head >
              < style >
                  body {
                  font - family: Arial, sans - serif;
                margin: 0;
                padding: 0;
                color: #333;
                  }
                  .container {
                  max - width: 600px;
                margin: 20px auto;
                padding: 20px;
                border: 1px solid #ddd;
                      border - radius: 5px;
                  background - color: #f9f9f9;
                  }
                  .header {
                  background - color: #007bff;
                      color: #ffffff;
                      padding: 10px;
                  text - align: center;
                  border - radius: 5px 5px 0 0;
                }
                  .footer {
                  background - color: #f2f2f2;
                      color: #888;
                      text - align: center;
                padding: 10px;
                  border - radius: 0 0 5px 5px;
                  font - size: 0.8em;
                }
                  .content {
                padding: 20px;
                  text - align: left;
                }
                a {
                color: #007bff;
                  }
              </ style >
          </ head >
          < body >
              < div class= 'container'>
                  <div class='header'>
                      <h2>Your Company Name</h2>
                  </div>
                  <div class='content'>
                      <h3>Hello" + Name + @",</h3>
                      <p>Thank you for [Action, e.g., signing up, making a purchase]. We're glad to have you with us. [Here you can add more details about the action or next steps.]</p>
                      <p>If you have any questions or need further assistance, please feel free to contact us at[Your Contact Information].</p>
                      <p>Best regards,<br>[Your Name or Your Company's Name]</p>
                  </div>
                  <div class='footer'>
                      This is an automated message.Please do not reply directly to this email.For assistance, please contact us at[Your Contact Email].
                  </div>
              </div>
          </body>
          </html>";
    }

		private readonly ILogger<HomeController> _logger;
		private const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=UserData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		[HttpGet]
		public IActionResult ConfiremEmailVerificaiton(string data) 
		{
			return Content("");
		}
		[HttpPost]
		public IActionResult SendVerificationEmail(EmailModel credentials)
		{
      EmailVerificationModel data = new EmailVerificationModel();
      data.Id = credentials.Email;
      //AuthID will be obtained from the database bacause AUTOINCREMENT is used in the database
      byte[] randArr = new byte[32];
      RandomNumberGenerator.Create().GetBytes(randArr);
      data.RandNum = Encoding.Unicode.GetString(randArr);
      data.ExpTime = DateTime.Now.ToOADate();
      
      MimeMessage msg = new MimeMessage();
      msg.From.Add(new MailboxAddress("c", "noreply.experimaentalsender@gmail.com"));
      msg.To.Add(new MailboxAddress("lalalei", "borodinsnikita@gmail.com"));
      msg.Body = new TextPart("html") { Text = CreateEmail("sample","","") };
      using (SmtpClient client = new SmtpClient())
      {
        client.LocalDomain = "smtp.gmail.com";
        client.Connect("smtp.gmail.com", 587);
        client.Authenticate("noreply.experimaentalsender@gmail.com", "umdtkhzoflcnhruw");
        client.Send(msg);
        client.Disconnect(true);

        return Content("");
      }
    }
		[HttpPost]
		public IActionResult CreateAccounr(AccountCreationModel credentials)
		{




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
					Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(credentials.Password,salt,1);
					hash = hasher.GetBytes(64);

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
		private static string IsAuthenticated(IRequestCookieCollection cookies,IPAddress ip)
		{
			return "";
			string data = cookies["auth"];
			if (data == null)
			{
				return "false";
			}
			AuthenticationJsonModel AuthCookie = JsonConvert.DeserializeObject<AuthenticationJsonModel>(data);
			string key = AuthCookie.Key;
			AesEngine e = new AesEngine();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}