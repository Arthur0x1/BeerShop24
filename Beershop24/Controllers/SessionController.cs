using Beershop24.Extensions;
using Beershop24.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Beershop24.Controllers
{
	public class SessionController : Controller
	{
		public IActionResult Index()
		{
			// HttpContext.Session.Set("aa", /* raw bytes only */);
			// write our own extension method
			HttpContext.Session.SetObject(
				"mySession",
				new SessionVM { Date = DateTime.Now, Company = "VIVES" }
			);
			return View();
		}
	}
}
