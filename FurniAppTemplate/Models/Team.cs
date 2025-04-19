namespace FurniAppTemplate.Models
{
    public class Team:BaseEntity
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
