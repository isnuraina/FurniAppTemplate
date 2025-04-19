using FurniAppTemplate.Data;
using FurniAppTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniAppTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly FurnitureDbContext _furnitureDbContext;
        public HomeController(FurnitureDbContext furnitureDbContext)
        {
            _furnitureDbContext = furnitureDbContext;
        }
        public IActionResult Index(string page="Home")
        {
            var homeVM = new HomeVM()
            {
                MainChairAndContents = _furnitureDbContext.MainChairAndContents
                .AsNoTracking()
                .FirstOrDefault(x => x.PageName == page),
                MainChairImageUrls = _furnitureDbContext.MainChairImageUrls.AsNoTracking().FirstOrDefault(),
                Blogs = _furnitureDbContext.Blogs.Take(3).ToList(),
                Furnitures = _furnitureDbContext.Furnitures.Take(3).ToList()
            };
           
            return View(homeVM);
        }
    }
}
