namespace AsianRestaurantProject.Models
{
    public class DataElementModel
    {
        public string name;
        public string description;
        public float cost;
        public int ind;
        public DataElementModel(string name, string description, float cost, int ind)
        {
            this.name = name;
            this.description = description;
            this.cost = cost;
            this.ind = ind;
        }
    }
}
