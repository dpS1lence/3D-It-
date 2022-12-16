using BlenderParadise.Models;
using BlenderParadise.Services;
using BlenderParadise.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace BlenderParadise.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IChallengeService _challengeService;

        public HomeController(IProductService productService, IChallengeService challengeService)
        {
            _productService = productService;
            _challengeService = challengeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _challengeService.GetChallengeAsync();

            return View(model);
        }

        public IActionResult Index(string value)
        {
            return RedirectToAction(nameof(FiltersPage), new { value });
        }

        public async Task<IActionResult> FiltersPage(string value)
        {
            var models = await _productService.SearchProductAsync(value);

            return View(models);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string details)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Details = details });
        }
    }
}