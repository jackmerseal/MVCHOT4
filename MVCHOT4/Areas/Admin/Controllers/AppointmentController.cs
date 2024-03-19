﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCHOT4.Models;

namespace MVCHOT4.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AppointmentController : Controller
	{
		private readonly AppointmentContext _context;

		public AppointmentController(AppointmentContext context)
		{
			_context = context;
		}
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
				vm.Customers = _context.Customers.ToList();
				return View(vm);
			}
		}

		[HttpGet]
		public IActionResult Edit(int? id, AppointmentViewModel vm)
		{
			vm.Customers = _context.Customers.ToList();
			if (id.HasValue)
			{
				var appointment = _context.Appointments.Find(id.Value);
				if (appointment != null)
				{
					return View("Edit", vm.Appointment);
				}
			}
			return View("Edit", new Appointment());
		}

		[HttpPost]
		public IActionResult Edit(AppointmentViewModel vm)
		{
			if (ModelState.IsValid)
			{
				if (vm.Id != 0)
				{
					_context.Appointments.Update(vm.Appointment);
				}
				_context.SaveChanges();
				return RedirectToAction("Appointments");
			}

			return View("Edit", vm.Appointment);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var appointment = _context.Appointments.Find(id);
			if (appointment != null)
			{
				return View(appointment);
			}
			return RedirectToAction("Appointments");
		}

		[HttpPost]
		public IActionResult DeleteAppointment(int id)
		{
			var appointment = _context.Appointments.Find(id);
			if (appointment != null)
			{
				_context.Appointments.Remove(appointment);
				_context.SaveChanges();
			}
			return RedirectToAction("Appointments");
		}
	}
}