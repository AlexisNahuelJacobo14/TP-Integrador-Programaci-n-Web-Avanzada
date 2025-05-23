using Integrador_Web_Avanz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Integrador_Web_Avanz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Partners()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

		public IActionResult FAQ()
		{
			return View();
		}

		public IActionResult Testimonios()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
