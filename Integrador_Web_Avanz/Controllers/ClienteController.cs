using Microsoft.AspNetCore.Mvc;
using Integrador_Web_Avanz.Models;
using Microsoft.EntityFrameworkCore;

namespace Integrador_Web_Avanz.Controllers
{
    public class ClienteController : Controller
    {
		private readonly PwaOkContext _DbContext;
		public ClienteController(PwaOkContext context) {

            _DbContext = context;
        }
        

        [HttpGet]
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear(Cliente cliente)
        {
            TempData["Mensaje"] = $"Se cargaron satisfactoriamente los datos: {cliente.Nombre} | {cliente.Apellido} | {cliente.Mail} | {cliente.Telefono}";
            return View("~/Views/Home/Contacto.cshtml", cliente);
            /*ClienteVM _cliente = new ClienteVM(),
            {

            }
            return View();*/
		}


    }

    


}
