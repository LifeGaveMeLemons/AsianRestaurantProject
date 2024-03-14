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
	}
}
