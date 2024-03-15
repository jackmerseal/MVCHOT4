using Microsoft.AspNetCore.Mvc;

namespace MVCHOT4.Controllers
{
	public class CustomerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
