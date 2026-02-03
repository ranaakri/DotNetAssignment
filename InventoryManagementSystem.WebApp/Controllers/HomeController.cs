using InventoryManagementSystem.BusinessLogic.Services;
using InventoryManagementSystem.Entities.Models;
using InventoryManagementSystem.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagementSystem.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            ViewBag.TotalProducts = _productService.GetTotalProductAsync().Result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListProduct()
        {
            var p = await _productService.GetAllProductsAsync();
            return View(p);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _productService.InsertProductAsync(product);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
