using Microsoft.AspNetCore.Mvc;

namespace EChallanSystem.Controllers
{
    public class EChallan : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
