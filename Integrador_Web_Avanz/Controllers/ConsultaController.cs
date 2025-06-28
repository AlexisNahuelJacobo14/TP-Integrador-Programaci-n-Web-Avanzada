using Microsoft.AspNetCore.Mvc;
using Integrador_Web_Avanz.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Integrador_Web_Avanz.Controllers
{
	public class ConsultaController : Controller
	{
		private readonly PwaOkContext _DbContext;

		public ConsultaController( PwaOkContext dbContext)
		{
			_DbContext = dbContext;
		}

		public IActionResult Index()
		{
			//List<Consulta> lista = _DbContext.Consulta.ToList();
			//List<Consulta> lista = _DbContext.Consulta.Include(c => c.Cliente).ToList();
			return View();
		}

		[HttpGet]
        public IActionResult Editar(int idConsulta)
        {
			ViewBag.TiposConsulta = new SelectList(new List<string>
			{
				"Desarrollo Web",
				"Data",
				"Desarrollo Apps"
			});
			ClienteConsultaVM model = new ClienteConsultaVM();
			//Consulta model = new Consulta();
			if (idConsulta != 0)
			{
				//model = _DbContext.Consulta.Find(idConsulta);
			}
			return View(model);
        }
        
		[HttpPost]
        public IActionResult Editar(ClienteConsultaVM model)
        {
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			else
			{
				// Declaramos las variables
				var cliente = model.Cliente;
				var consulta = model.Consulta;

				if (cliente.IdCliente == 0)
				{
					// Agregamos el cliente
					_DbContext.Clientes.Add(cliente);
					_DbContext.SaveChanges();
				}
				else
				{
					// Updateamos el cliente
					_DbContext.Clientes.Update(cliente);
				}
				
				if(model.Consulta.IdConsulta == 0)
				{
					// Agregamos la consulta con FK al cliente
					
					consulta.IdCliente = cliente.IdCliente;

					_DbContext.Consulta.Add(consulta);
					_DbContext.SaveChanges();
				}
				else
				{
					_DbContext.Consulta.Update(consulta);
				}

				//return RedirectToAction("Index", "Home");
				return RedirectToAction("Consultas");
			}
            
        }

		[HttpGet]
		public IActionResult Eliminar(int idConsulta)
		{
			Consulta Consulta = new Consulta();
			if (idConsulta != 0)
			{
				Consulta = _DbContext.Consulta.Find(idConsulta);
			}
			return View(Consulta);
		}

		[HttpPost]
		public IActionResult Eliminar(Consulta modelConsulta)
		{
			_DbContext.Consulta.Remove(modelConsulta);
			_DbContext.SaveChanges();
			//return RedirectToAction("Index", "Home");
            return RedirectToAction("Consultas");
        }

        public IActionResult Consultas()
        {
			List<Consulta> lista = _DbContext.Consulta.Include(c => c._Cliente).ToList();
            return View(lista);
        }
    }
}
