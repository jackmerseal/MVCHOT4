using Microsoft.AspNetCore.Mvc;

namespace MVCHOT4.Controllers
{
	public class AppointmentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
