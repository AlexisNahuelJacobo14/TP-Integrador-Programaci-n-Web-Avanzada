using Microsoft.AspNetCore.Mvc;
using Integrador_Web_Avanz.Models;
using System.Security.Cryptography.X509Certificates;

namespace Integrador_Web_Avanz.Controllers
{
    public class PartnerController : Controller
    {

        
        public IActionResult Partners()
        {
            var partners = new List<Partner>
            {
            new Partner("Qlik Sense", "/Imagenes/logo-qlik-color.png"),
            new Partner("Snow Flake", "/Imagenes/Snowflake-Emblem.png"),
            new Partner("Tienda Nube", "/Imagenes/tienda-nube-logo.png")
            };
            return View(partners);

        }
    }
}
