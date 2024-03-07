using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AsianRestaurantProject.Models
{
    public class DataElementModel
    {
        [JsonProperty("Name")]
        public string Name { get; }
        [JsonProperty("Description")]
        public string Description { get; }
        [JsonProperty("Cost")]
        public float Cost { get; }
        [JsonProperty("iid")]
        public int Iid { get; }
        public DataElementModel(string name, string description, float cost, int iid)
        {
            this.Name = name;
            this.Description = description;
            this.Cost = cost;
            this.Iid = iid;
        }
    }
}
