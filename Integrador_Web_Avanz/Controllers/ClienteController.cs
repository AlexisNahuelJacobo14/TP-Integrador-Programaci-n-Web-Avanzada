using Microsoft.AspNetCore.Mvc;
using Integrador_Web_Avanz.Models;

namespace Integrador_Web_Avanz.Controllers
{
    public class ClienteController : Controller
    {
        

        [HttpGet]
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            TempData["Mensaje"] = $"Se cargaron satisfactoriamente los datos: {cliente.Nombre} | {cliente.Apellido} | {cliente.Mail} | {cliente.Telefono}";
            return View("~/Views/Home/Contacto.cshtml", cliente);
        }

    }
}
