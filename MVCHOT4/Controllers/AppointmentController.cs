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
            var vm = new AppointmentViewModel();
            vm.Customers = _context.Customers.ToList();
            return View("Add", vm);
        }

        [HttpPost]
        public IActionResult Add(AppointmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
				if (vm.Id == 0)
				{
					_context.Add(vm.Appointment);
				}
				_context.SaveChanges();
				return RedirectToAction("Appointments");
			}
            else
            {
                ViewBag.Action = "Add Appointment";
                vm.Appointments = _context.Appointments.ToList();
                return View("Add", vm);
            }
        }
    }
}
