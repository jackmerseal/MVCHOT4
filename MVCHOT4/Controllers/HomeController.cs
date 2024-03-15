using Microsoft.AspNetCore.Mvc;
using MVCHOT4.Models;
using System.Diagnostics;

namespace MVCHOT4.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}