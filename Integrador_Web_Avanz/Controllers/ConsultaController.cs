using Microsoft.AspNetCore.Mvc;
using Integrador_Web_Avanz.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Integrador_Web_Avanz.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Net;

namespace Integrador_Web_Avanz.Controllers
{
	public class ConsultaController : Controller
	{
		List<Item> MyCart = new List<Item>();
		public decimal total = 0;
		int contar = 0;

		private readonly PwaOkContext _DbContext;

		public ConsultaController( PwaOkContext dbContext)
		{
			_DbContext = dbContext;
		}

		public int ContarItems(List<Item> items)
		{
			int cantidad = items.Count();
			return cantidad;
		}

		private int Exists(List<Item> cart, int id)
		{
			for(int i = 0; i < cart.Count; i++)
			{
                if (cart[i]._Consulta.IdTipoConsulta.Equals(id))
                {
					return i;
				}
			}
			return -1;
		}

		public IActionResult VistaCarrito()
		{
			int cantidad;

			var cart = Helpers.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
			if(cart == null)
			{
				//int cantConsultas = _DbContext.Consulta.ToList().Count();
				//cantidad = cantConsultas;
				cantidad = 0;
			}
			else
			{
				cantidad = cart.Count();
			}
			TempData["Contar"] = cantidad;
			var listaConsultas = _DbContext.Consulta
				.Include(c => c.IdTipoConsultaNavigation)
				.ToList();
			var listaItems = listaConsultas.Select(tc => new Item()
			{
				_Consulta = tc,
				Cantidad = cantidad
			});
			return View(listaItems);
		}

		[HttpGet]
		public IActionResult Carrito()
		{
			var MyCart = Helpers.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

			foreach (var c in MyCart) {
				var _idTipoConsulta = c._Consulta.IdTipoConsulta;
				if(_idTipoConsulta != null)
				{
					TipoConsulta tConsul = new TipoConsulta();
					tConsul = _DbContext.TipoConsultas.Find(_idTipoConsulta);
					if (tConsul != null)
					{
						c._Consulta.IdTipoConsultaNavigation = tConsul;
					}
				}
			} 

			if(MyCart == null)
			{
				return RedirectToAction("VistaCarrito");
			}
			else
			{
				return View("VistaCarrito",MyCart);
			}
		}
		[HttpGet]
		public IActionResult Cart(int id)
		{
			var _consult = _DbContext.Consulta.Find(id);
			var cart = Helpers.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
			if(cart == null)
			{
				cart = new List<Item>();
				cart.Add(new Item()
				{
					_Consulta = _consult,
					Cantidad = 1
				});

				TempData["Contar"] = ContarItems(cart);

                Helpers.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			}
			else
			{
				int index = Exists(cart, id);
				if (index == -1)
				{
					cart.Add(new Item()
					{
						_Consulta = _consult,
						Cantidad = 1
					});
				}
				else
				{
					var newCantidad = cart[index].Cantidad + 1;
					cart[index].Cantidad = newCantidad;
				}

				TempData["Contar"] = ContarItems(cart);
                Helpers.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			}

			return RedirectToAction("Carrito");
		}
		[HttpGet]
		public IActionResult Quitar(int idCarrito)
		{
			var cart = Helpers.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

			int index = Exists(cart, idCarrito);
			cart.RemoveAt(index);

			TempData["Contar"] = ContarItems(cart);
            Helpers.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
			return RedirectToAction("Carrito");

		}


		[HttpGet]
        public IActionResult Contacto(int idConsulta)
        {

			ClienteConsultaVM model = new ClienteConsultaVM()
			{
				_listaPartners = _DbContext.Partners.Select(partner => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = partner.Marca,
					Value = partner.IdPartner.ToString()
				}).ToList(),

				_listaTiposDeConsulta = _DbContext.TipoConsultas.ToList()

		};
			// Intentar recuperar el DNI del cliente desde la sesión
			string dniGuardado = HttpContext.Session.GetString("dniCliente");

			if (!string.IsNullOrEmpty(dniGuardado) && int.TryParse(dniGuardado, out int dni))
			{
				var cliente = _DbContext.Clientes.FirstOrDefault(c => c.Dni == dni);
				if (cliente != null)
				{
					model.Cliente = cliente;
				}
			}

			return View(model);
        }
        
		[HttpPost]
        public IActionResult Contacto(ClienteConsultaVM model)
        {
			if (!ModelState.IsValid)
			{
				model._listaPartners = _DbContext.Partners.Select(partner => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = partner.Marca,
					Value = partner.IdPartner.ToString()
				}).ToList();

				model._listaTiposDeConsulta = _DbContext.TipoConsultas.ToList();


				return View(model);
			}
			else
			{
				// Declaramos las variables
				
				var consulta = model.Consulta;

				Cliente clienteExistente = _DbContext.Clientes.FirstOrDefault(c => c.Dni == model.Cliente.Dni);

				// Si el cliente no existe, lo agregamos a la BD 

				if (clienteExistente == null)
				{
					// Agregamos el cliente
					_DbContext.Clientes.Add(model.Cliente);
					_DbContext.SaveChanges();
					consulta.IdCliente = model.Cliente.IdCliente;
				}
				else
				{
					// Cliente existente, actualizamos sus datos
					clienteExistente.Nombre = model.Cliente.Nombre;
					clienteExistente.Apellido = model.Cliente.Apellido;
					clienteExistente.Mail = model.Cliente.Mail;

					_DbContext.Clientes.Update(clienteExistente);
					_DbContext.SaveChanges();

					consulta.IdCliente = clienteExistente.IdCliente;
				}

				// Agregamos la consulta con FK al cliente
				_DbContext.Consulta.Add(consulta);
				_DbContext.SaveChanges();

				Cart(consulta.IdConsulta);
				HttpContext.Session.SetString("dniCliente", model.Cliente.Dni.ToString());

				TipoConsulta tipoConsulta = _DbContext.TipoConsultas.Find(consulta.IdTipoConsulta);
				TempData["Mensaje"] = $"Consulta de {tipoConsulta.TipoDeConsulta} agregada a tu carrito, podes agregar más o ir a finalizar tu compra. \t ¡Muchas gracias {model.Cliente.Nombre}!";
				TempData["MensajeDatos"] = $"¡Atención {model.Cliente.Nombre}! Sus datos ya se almacenaron, si los modifica se actualizaran.";


				return RedirectToAction("Contacto");
			}
            
        }

		

		[HttpGet]
		public IActionResult Eliminar(int idConsulta)
		{
			Consulta Consulta = new Consulta();
			if (idConsulta != 0)
			{
				Consulta = _DbContext.Consulta
					.Include(c => c.IdTipoConsultaNavigation)
					.Include(c => c.IdClienteNavigation)
					.FirstOrDefault(c => c.IdConsulta == idConsulta);

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

		[HttpGet]
        public IActionResult Consultas()
        {
			// Intentar recuperar el DNI del cliente desde la sesión
			string dniGuardado = HttpContext.Session.GetString("dniCliente");
			Cliente clienteConsulta = new Cliente();

			if (!string.IsNullOrEmpty(dniGuardado) && int.TryParse(dniGuardado, out int dni))
			{
				var cliente = _DbContext.Clientes.FirstOrDefault(c => c.Dni == dni);
				if (cliente != null)
				{
					clienteConsulta = cliente;
				}
			}

			ClienteConsultaVM model = new ClienteConsultaVM();
			model._listaConsultas = _DbContext.Consulta
				.Include(c => c.IdClienteNavigation)
				.Include(c => c.IdPartnerNavigation)
				.Include(c => c.IdTipoConsultaNavigation)
				.Where(c => c.IdCliente == clienteConsulta.IdCliente)
				.ToList();
            return View(model);
        }
		[HttpPost]
		public IActionResult Consultas(Cliente cliente)
		{
			Cliente clienteConsulta = _DbContext.Clientes.FirstOrDefault(c => c.Dni == cliente.Dni);
			ClienteConsultaVM model = new ClienteConsultaVM();
			if (clienteConsulta != null)
			{
				
				model._listaConsultas = _DbContext.Consulta
					.Include(c => c.IdClienteNavigation)
					.Include(c => c.IdPartnerNavigation)
					.Include(c => c.IdTipoConsultaNavigation)
					.Where(c => c.IdCliente == clienteConsulta.IdCliente)
					.ToList();

				if(model._listaConsultas.Count == 0)
				{
					TempData["ClienteSinConsultas"] = $"¡Bienvenido {clienteConsulta.Nombre}! Aún no recibimos consultas bajo tu DNI, podes hacerlo desde la sección Contacto.";
				}
				HttpContext.Session.SetString("dniCliente", model.Cliente.Dni.ToString());
			}
			else
			{
				TempData["ClienteNoExistente"] = $"El DNI {cliente.Dni} no existe en nuestra base de datos.";
			}
			
			return View(model);
		}
	}
}
