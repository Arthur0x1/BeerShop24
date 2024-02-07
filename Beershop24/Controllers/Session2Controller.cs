using Beershop24.Extensions;
using Beershop24.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Beershop24.Controllers
{
	public class Session2Controller : Controller
	{
		public IActionResult Index()
		{
			var sessionVM = HttpContext.Session.GetObject<SessionVM>("mySession");
			if (sessionVM != null)
			{
				return View(sessionVM);
			}
			return View();
		}
	}
}
