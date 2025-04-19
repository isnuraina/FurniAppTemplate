using FurniAppTemplate.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniAppTemplate.Controllers
{
    public class ShopController : Controller
    {
        private readonly FurnitureDbContext _furnitureDbContext;
        public ShopController(FurnitureDbContext furnitureDbContext)
        {
            _furnitureDbContext = furnitureDbContext;
        }
        public IActionResult Index()
        {
            var shops = _furnitureDbContext.Furnitures.ToList();
            return View(shops);
        }
    }
}
