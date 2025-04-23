using FurniAppTemplate.Data;
using FurniAppTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FurniAppTemplate.Controllers
{
    public class BasketController : Controller
    {
        private readonly FurnitureDbContext _furnitureDbContext;

        public BasketController(FurnitureDbContext furnitureDbContext)
        {
            _furnitureDbContext = furnitureDbContext;
        }

        public IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> basketList;

            if (basket is null)
            {
                basketList = new();
            }
            else
            {
                basketList = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }

            var ids = basketList.Select(b => b.Id).ToList();

            var dbProducts = _furnitureDbContext.Furnitures
                .Where(p => ids.Contains(p.Id))
                .ToList();

            var updatedBasket = new List<BasketVM>();

            foreach (var item in basketList)
            {
                var product = dbProducts.FirstOrDefault(p => p.Id == item.Id);
                if (product != null)
                {
                    updatedBasket.Add(new BasketVM
                    {
                        Id = product.Id,
                        BasketCount = item.BasketCount,
                        Title = product.Title,
                        Price = product.Price,
                        Image = product.ImageURL,
                        Description = product.Description
                    });
                }
            }
            TempData["BasketCount"] = updatedBasket.Sum(x => x.BasketCount);

            return View(updatedBasket);
        }


        public IActionResult AddBasket(int? id)
        {
            if (id == null) return BadRequest();
            var existProduct = _furnitureDbContext.Furnitures.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return BadRequest();
            string basket = Request.Cookies["basket"];
            List<BasketVM> list;
            if (basket is null)
            {
                list = new();
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            var existProductBasket = list.FirstOrDefault(p => p.Id == existProduct.Id);
            if (existProductBasket == null)
            {
                list.Add(new BasketVM() { Id = existProduct.Id, BasketCount = 1,Title=existProduct.Title });
            }
            else
            {
                existProductBasket.BasketCount++;
            }
            TempData["BasketCount"] = list.Sum(x => x.BasketCount);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(list));
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Remove(int? id)
        {
            if (id == null)
                return BadRequest();

            string basket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basket))
                return RedirectToAction("ShowBasket");

            List<BasketVM> basketList = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            var productToRemove = basketList.FirstOrDefault(x => x.Id == id);
            if (productToRemove != null)
            {
                basketList.Remove(productToRemove);

                string updatedBasket = JsonConvert.SerializeObject(basketList);
                Response.Cookies.Append("basket", updatedBasket);
            }

            return RedirectToAction("ShowBasket");
        }

    }
}
