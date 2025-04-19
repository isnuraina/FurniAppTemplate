namespace FurniAppTemplate.Models
{
    public class Blog:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}
