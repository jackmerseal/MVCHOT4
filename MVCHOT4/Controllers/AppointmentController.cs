using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCHOT4.Models;

namespace MVCHOT4.Controllers
{
	public class AppointmentController : Controller
	{
		private readonly AppointmentContext _context;
        public AppointmentController(AppointmentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Appointments()
        {
            IQueryable<Appointment> query = _context.Appointments.Include(a => a.Customer);
            var model = new AppointmentViewModel
            {
                Appointments = query.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Appointments(AppointmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(vm.Appointment);
                _context.SaveChanges();
                return RedirectToAction("Appointments");
            }
            else
            {
                return View(vm.Appointment);
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
			ViewBag.Action = "Add Appointment";
			ViewBag.Appointments = _context.Appointments.OrderBy(a => a.StartTime).ToList();
			return View("Add", new Appointment());
		}

        [HttpPost]
        public IActionResult Add(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (appointment.Id == 0)
                {
                    _context.Appointments.Add(appointment);
                }
                _context.SaveChanges();
                return RedirectToAction("Appointments");
            }

            return View("Add", appointment);
        }
    }
}
