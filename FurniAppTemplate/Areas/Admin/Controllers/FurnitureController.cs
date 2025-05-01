using FurniAppTemplate.Areas.Admin.ViewModels.Furniture;
using FurniAppTemplate.Data;
using FurniAppTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FurniAppTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FurnitureController : Controller
    {
        public FurnitureDbContext _furnitureDbContext;

        public FurnitureController(FurnitureDbContext furnitureDbContext)
        {
            _furnitureDbContext = furnitureDbContext;
        }
        public IActionResult Index()
        {
            var furnitures=_furnitureDbContext.Furnitures.ToList();
            return View(furnitures);
        }
        public IActionResult Detail(int? id)
        {
            if (id is null) return BadRequest();

            var furniture = _furnitureDbContext.Furnitures.FirstOrDefault(f => f.Id == id);
            if (furniture is null) return NotFound();

            return View(furniture);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult > Create(FurnitureCreateVM furniture)
        {
            var file = furniture.Photo;
            if (file==null)
            {
                ModelState.AddModelError("Photo", "bos ola bilmez");
                return View(furniture);
            }
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using FileStream fileStream = new(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            var newFurniture = new Furniture()
            {
                Title = furniture.Title,
                Price=furniture.Price,
                ImageURL = fileName,
                Description =furniture.Description
            };
            await _furnitureDbContext.Furnitures.AddAsync(newFurniture);
            await _furnitureDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var furniture = _furnitureDbContext.Furnitures.FirstOrDefault(f => f.Id == id);
            if (furniture is null) return NotFound();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", furniture.ImageURL);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _furnitureDbContext.Furnitures.Remove(furniture);
            await _furnitureDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if(id is null) return BadRequest();
            var furniture =await _furnitureDbContext.Furnitures.FirstOrDefaultAsync(f => f.Id == id);
            if (furniture is null) return NotFound();
            return View(new FurnitureUpdateVM { Description=furniture.Description,Title=furniture.Title,Price=furniture.Price,ImageURL=furniture.ImageURL});
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,FurnitureUpdateVM furnitureUpdateVM)
        {
            if (id is null) return BadRequest();
            var furniture = await _furnitureDbContext.Furnitures.FirstOrDefaultAsync(f => f.Id == id);
            if (furniture is null) return NotFound();
            var file = furnitureUpdateVM.Photo;
            if (file == null)
            {
                ModelState.AddModelError("Photo", "Şəkil boş ola bilməz");
                furnitureUpdateVM.ImageURL = furniture.ImageURL;
                return View(furnitureUpdateVM);
            }
            string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", furniture.ImageURL);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            furniture.ImageURL = fileName;
            furniture.Price = furnitureUpdateVM.Price;
            furniture.Description = furnitureUpdateVM.Description;
            furniture.Title = furnitureUpdateVM.Title;
            await _furnitureDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
