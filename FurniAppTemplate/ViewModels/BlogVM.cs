using FurniAppTemplate.Models;

namespace FurniAppTemplate.ViewModels
{
    public class BlogVM
    {
        public MainChairImageUrl MainChairImageUrls { get; set; }
        public MainChairAndContent MainChairAndContents { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
