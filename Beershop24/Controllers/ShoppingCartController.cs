using Beershop24.Extensions;
using Beershop24.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Beershop24.Controllers
{
	public class ShoppingCartController : Controller
	{
		public IActionResult Index()
		{
			var cart = HttpContext.Session.GetObject<ShoppingCartVM>("shoppingCart");
			// Session id = HttpContext.Session.Id
			return View(cart);
		}

		public IActionResult Delete(int? id)
		{
			return View();
		}
	}
}
