namespace FurniAppTemplate.Areas.Admin.ViewModels.Furniture
{
    public class FurnitureUpdateVM
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
