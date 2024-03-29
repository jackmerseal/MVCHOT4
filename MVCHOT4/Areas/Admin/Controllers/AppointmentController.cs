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
			if (vm.Id != 0)
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
		public IActionResult Edit(int? id)
		{
			if (id.HasValue)
			{
				var appointment = _context.Appointments.Find(id.Value);
				if (appointment != null)
				{
					var vm = new AppointmentViewModel
					{
						Appointment = appointment,
						Customers = _context.Customers.ToList()
					};
					return View("Edit", vm);
				}
			}
			return View("Edit", new AppointmentViewModel());
		}

		[HttpPost]
		public IActionResult Edit(AppointmentViewModel vm)
		{
			if (vm.Appointment.Id != 0)
			{
				_context.Appointments.Update(vm.Appointment);
				_context.SaveChanges();
				return RedirectToAction("Appointments");
			}
			else
			{
				return RedirectToAction("Edit", new { id = vm.Appointment.Id });
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var appointment = _context.Appointments.Find(id);
			if (appointment == null)
			{
				return RedirectToAction("Appointments");
			}

			var vm = new AppointmentViewModel
			{ 
				Appointment = appointment 
			};
			return View(vm);
		}

		[HttpPost]
		public IActionResult Delete(AppointmentViewModel vm)
		{
			if(vm.Appointment.Id != 0)
			{
				var appointment = _context.Appointments.Find(vm.Appointment.Id);
				if (appointment != null)
				{
					_context.Appointments.Remove(appointment);
					_context.SaveChanges();
				}
			}
			return RedirectToAction("Appointments");
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