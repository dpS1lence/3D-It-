using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Recents()
        {
            var model = (await _productService.GetAllAsync()).TakeLast(4).ToList();

            return View(model);
        }
    }
}
