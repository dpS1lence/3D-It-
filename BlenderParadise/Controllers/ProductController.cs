using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await _productService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> One(int id)
        {
            var model = await _productService.GetOneAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
