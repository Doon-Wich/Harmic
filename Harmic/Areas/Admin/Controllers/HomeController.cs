using Microsoft.AspNetCore.Mvc;
using Harmic.Utilities;

namespace Harmic.Areas.Admin.Controllers
{

	[Area("Admin")]


	public class HomeController : Controller
	{
		public IActionResult Index()
		{
            if (Function.account == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
		}
	}
}
