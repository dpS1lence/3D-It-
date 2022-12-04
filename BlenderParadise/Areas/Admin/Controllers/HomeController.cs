using Microsoft.AspNetCore.Mvc;

namespace BlenderParadise.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
