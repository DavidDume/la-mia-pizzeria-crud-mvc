namespace la_mia_pizzeria_crud_mvc.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }

        public Pizza() { }
        public Pizza(string name, string description, string image, float price)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
            this.Price = price;
        }
    }
}
