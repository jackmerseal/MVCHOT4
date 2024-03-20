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
                return View(vm);
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            var vm = new AppointmentViewModel();
            vm.Customers = _context.Customers.ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add([FromForm] AppointmentViewModel vm)
        {
                if (vm.Appointment.Id != null)
                {
                    _context.Appointments.Add(vm.Appointment);
                    _context.SaveChanges();
                    return RedirectToAction("Appointments");
                }
                else
                {
                    var isTimeSlotAvailableAttribute = vm.GetType().GetProperty("StartTime").GetCustomAttributes(typeof(IsTimeSlotAvailableAttribute), true).FirstOrDefault() as IsTimeSlotAvailableAttribute;
                    if (isTimeSlotAvailableAttribute != null)
                    {
                        ModelState.AddModelError("Appointment.StartTime", isTimeSlotAvailableAttribute.ErrorMessage);
                        vm.Customers = _context.Customers.ToList();
                        return View(vm);
                    }
                }
                vm.Customers = _context.Customers.ToList();
                return View(vm);
        }

		[HttpGet]
		public IActionResult Cancel(int id)
		{
			if (id == 0)
			{
				return RedirectToAction("Appointments");
			}
			else
			{
				return RedirectToAction("Index", new { id });
			}
		}
	}
}
