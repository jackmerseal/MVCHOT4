using Microsoft.AspNetCore.Mvc;
using MVCHOT4.Models;

namespace MVCHOT4.Controllers
{
	public class CustomerController : Controller
	{
		private readonly AppointmentContext _context;
		public CustomerController(AppointmentContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Customers()
		{
			var customers = _context.Customers.OrderBy(c => c.Username).ToList();
			return View(customers);
		}

		[HttpPost]
        public IActionResult Customers(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Customers");
            }
            else
            {
                return View(customer);
            }
        }

		[HttpGet]
		public IActionResult Add(int? id)
		{
			if (id.HasValue)
			{
				var customer = _context.Customers.Find(id.Value);
				if (customer != null)
				{
					return View("Add", customer);
				}
			}
			return View("Add", new Customer());
		}

		[HttpPost]
		public IActionResult Add(Customer customer)
		{
			if (ModelState.IsValid)
			{
				if (customer.CustomerId == 0)
				{
					_context.Customers.Add(customer);
				}

				_context.SaveChanges();
				return RedirectToAction("Customers");
			}

			return View("Add", customer);
		}
	}
}
