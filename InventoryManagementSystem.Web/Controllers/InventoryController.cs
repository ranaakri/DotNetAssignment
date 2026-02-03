using InventoryManagementSystem.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IProductService _productService;

        public InventoryController(IProductService productService)
        {
            _productService = productService;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var data = _productService.GetAllProductsAsync();
        //    return View(data);
        //}
    }
}
