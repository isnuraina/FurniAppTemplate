using FurniAppTemplate.Data;
using Microsoft.AspNetCore.Mvc;

namespace FurniAppTemplate.Controllers
{
    public class AboutController : Controller
    {
        private readonly FurnitureDbContext _furnitureDbContext;
        public AboutController(FurnitureDbContext furnitureDbContext)
        {
            _furnitureDbContext = furnitureDbContext;
        }
        public IActionResult Index()
        {
            var teams = _furnitureDbContext.Teams.Take(3).ToList();
            return View(teams);
        }
    }
}
