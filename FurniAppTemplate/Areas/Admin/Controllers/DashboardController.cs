using FurniAppTemplate.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FurniAppTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public FurnitureDbContext _furnitureDbContext;

        public DashboardController(FurnitureDbContext furnitureDbContext)
        {
            _furnitureDbContext = furnitureDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var furnitures =await _furnitureDbContext.Furnitures.ToListAsync();
            return View(furnitures);
        }
    }
}
