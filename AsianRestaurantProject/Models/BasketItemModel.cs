using Newtonsoft.Json;

namespace AsianRestaurantProject.Models
{
	public class BasketItemModel
	{
		[JsonProperty("Name")]
		public string Name { get; }
		[JsonProperty("Description")]
		public string Description { get; }
		[JsonProperty("Cost")]
		public float Cost { get; }
		[JsonProperty("iid")]
		public int Iid { get; }
		[JsonProperty("Quantity")]
		public int Quantity { get; }

		public BasketItemModel(string name, string description, float cost, int iid, int quantity)
		{
			this.Quantity = quantity;
			this.Name = name;
			this.Description = description;
			this.Cost = cost;
			this.Iid = iid;
		}
	}
}
