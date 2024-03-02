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
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace AsianRestaurantProject.Controllers
{
	public class HomeController : Controller
	{
    private string CreateEmail(string url)
    {
      return @"<!DOCTYPE html>
          <html>
          <head>
              <style>
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
              </style>
          </head>
          <body>
              <div class= 'container'>
                  <div class='header'>
                      <h2>Your Company Name</h2>
                  </div>
                  <div class='content'>
                      <h3>Hello</h3>
                      <p>this is  averification email for https:\//localhodt:7045</p>
                      <a href='https://localhost:7045/Home/ConfirmEmailVerificaiton?data=" + url+@"'>Click here to verify your email</a>
                      <p> if you did not create an account on this website with this email, please disregard this email</p>
                  </div>
                    "+ url +@"
                  <div class='footer'>
                      This is an automated message.Please do not reply directly to this email.For assistance, please contact us at[Your Contact Email].
                  </div>
              </div>
          </body>
          </html>";
    }

		private readonly ILogger<HomeController> _logger;
		private const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\borod\\source\\repos\\LifeGaveMeLemons\\AsianRestaurantProject\\AsianRestaurantProject\\Data\\Users\\UserDatabase.mdf;Integrated Security=True;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
		
		public IActionResult ConfirmEmailVerificaiton(string data) 
		{
      EmailVerificationModel verificationData = JsonConvert.DeserializeObject<EmailVerificationModel>(data);

            //verify integrity
            if (!verificationData.CheckKey())
            {
              //Todo: handle invalid signature
              return RedirectToAction("");
            }
      
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
              SqlCommand cmd = verificationData.GetRetrievalQuery(conn);
              conn.Open();
              cmd.ExecuteNonQuery();
              using (SqlDataReader reader = cmd.ExecuteReader())
              {
              }
        conn.Close();
            }
      //compare SQL
      return View();
		}
		[HttpPost]
		public IActionResult SendVerificationEmail(EmailModel credentials)
		{
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        EmailVerificationModel data = new EmailVerificationModel(credentials.Email);

       data.CreateKey();
      using (SqlConnection conn = new SqlConnection(connectionString))
      {
        
        SqlCommand cmd = data.GetInsertionQuery(conn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        cmd.Dispose();
      }

      string v = JsonConvert.SerializeObject(data);

      MimeMessage msg = new MimeMessage();
      msg.From.Add(new MailboxAddress("c", "noreply.experimaentalsender@gmail.com"));
      msg.To.Add(new MailboxAddress("lalalei", credentials.Email));
      msg.Body = new TextPart("html") { Text = CreateEmail(v) };
      using (SmtpClient client = new SmtpClient())
      {
        client.LocalDomain = "smtp.gmail.com";
        client.Connect("smtp.gmail.com", 587);
        client.Authenticate("noreply.experimaentalsender@gmail.com", "anltahpmtckxmqrb");
        client.Send(msg);
        client.Disconnect(true);

        return Content("");
      }
    }
    public IActionResult CreateAccount(AccountCreationModel credentials)
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
          Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(credentials.Password, salt, 1);
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
			return View(new List<DataElementModel>() { new DataElementModel("r", "r", 4f, 1),
      new DataElementModel("smileee","smile",5f,2),
      new DataElementModel("wireshark","yum!",5f,3),
      new DataElementModel("explorer!","surf the internet!",5f,4)});
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

    public IActionResult AboutUs()
    {
      return View();
    }
    public IActionResult ContactUs() 
    {
      return View();
    }
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}