using AsianRestaurantProject.Controllers;
using AsianRestaurantProject.Data;
using AsianRestaurantProject.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using Newtonsoft.Json;

namespace AsianRestaurantProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			string localDbPath = $"{Environment.ProcessPath.Substring(0,Environment.ProcessPath.Length- "\\bin\\Debug\\net6.0\\AsianRestaurantProject.exe".Length)}\\Data\\Users\\UserDatabase.mdf";

			HomeController.connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={localDbPath};Integrated Security=True;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
			app.MapControllerRoute(
				name: "VerifyEmail",
				pattern: "{controller=Home}/{action=ConfiremEmailVerificaiton}/{id?}");
			DataHolder.SetCryptoKey("hwiurhfewiurfh");
			HomeController.MenuItems = JsonConvert.DeserializeObject<DataElementModel[]>(File.ReadAllText(Environment.ProcessPath.Substring(0,Environment.ProcessPath.Length - 43) + "Data\\Items\\FoodItems.json"));
			app.Run();
		}
	}
}