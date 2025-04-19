using FurniAppTemplate.Data;
using FurniAppTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurniAppTemplate.Controllers
{
    public class BlogController : Controller
    {
        private readonly FurnitureDbContext _furnitureDbContext;
        public BlogController(FurnitureDbContext furnitureDbContext)
        {
            _furnitureDbContext = furnitureDbContext;
        }
        public IActionResult Index(string page = "Blog")
        {
            ViewBag.BlogCount = _furnitureDbContext.Blogs.Count();

            BlogVM datas = new BlogVM()
            {
                MainChairAndContents = _furnitureDbContext.MainChairAndContents
                .AsNoTracking()
                .FirstOrDefault(x => x.PageName == page),
                MainChairImageUrls = _furnitureDbContext.MainChairImageUrls.AsNoTracking().FirstOrDefault(),
                Blogs = _furnitureDbContext.Blogs.Take(3).AsNoTracking().ToList()
            };
            return View(datas);
        }
        public IActionResult Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var data = _furnitureDbContext.Blogs.FirstOrDefault(m => m.Id == id);
            if (data is null)
            {
                return NotFound();
            }
            return View(data);
        }
        public IActionResult LoadMore(int offset=3)
        {
            var datas = _furnitureDbContext.Blogs.Skip(offset).Take(3).ToList();
            return PartialView("_BlogPartialView", datas);
        }
        public IActionResult SearchBlog(string text)
        {
            var datas=_furnitureDbContext.Blogs
                .Where(b => b.Title.ToLower().Contains(text.ToLower()))
                .OrderByDescending(b => b.Id)
                .Take(3)
                .ToList();
            return PartialView("_SearchPartialView", datas);
        }
    }
}
