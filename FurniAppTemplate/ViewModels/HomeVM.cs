using FurniAppTemplate.Migrations;
using FurniAppTemplate.Models;

namespace FurniAppTemplate.ViewModels
{
    public class HomeVM
    {
        public MainChairImageUrl MainChairImageUrls { get; set; }
        public  MainChairAndContent MainChairAndContents { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Furniture> Furnitures { get; set; }
    }
}
