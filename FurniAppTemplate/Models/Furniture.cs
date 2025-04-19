namespace FurniAppTemplate.Models
{
    public class Furniture:BaseEntity
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
    }
}
