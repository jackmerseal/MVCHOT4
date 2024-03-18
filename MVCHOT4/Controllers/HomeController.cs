using Microsoft.AspNetCore.Mvc;
using MVCHOT4.Models;
using System.Diagnostics;

namespace MVCHOT4.Controllers
{
    public class HomeController : Controller
    {
        public AppointmentContext Context { get; set; }

        public HomeController(AppointmentContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            var appointments = Context.Appointments.OrderBy(a => a.StartTime).ToList();
            return View(appointments);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}