using Microsoft.AspNetCore.Mvc;
using Integrador_Web_Avanz.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
			List<Consulta> lista = _DbContext.Consulta.ToList();
			return View(lista);
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
			Consulta _consulta = new Consulta();
			if(idConsulta != 0)
			{
				_consulta = _DbContext.Consulta.Find(idConsulta);
			}
			return View(_consulta);
        }
        
		[HttpPost]
        public IActionResult Editar(Consulta modelConsulta)
        {
			if (!ModelState.IsValid)
			{
				return View(modelConsulta);
			}
			else
			{
				if (modelConsulta.IdConsulta == 0)
				{
					_DbContext.Consulta.Add(modelConsulta);
				}
				else
				{
					_DbContext.Update(modelConsulta);
				}
				_DbContext.SaveChanges();

				//return RedirectToAction("Index", "Home");
				return RedirectToAction("Consultas");
			}
            
        }

		[HttpGet]
		public IActionResult Eliminar(int idConsulta)
		{
			Consulta _consulta = new Consulta();
			if (idConsulta != 0)
			{
				_consulta = _DbContext.Consulta.Find(idConsulta);
			}
			return View(_consulta);
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
			List<Consulta> lista = _DbContext.Consulta.ToList();
            return View(lista);
        }
    }
}
