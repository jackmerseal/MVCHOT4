using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCHOT4.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        [FutureDate(ErrorMessage = "Appointment must be in the future")]
        [IsTimeSlotAvailable(ErrorMessage = "This appointment time is not available")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }
        [ValidateNever]
        public Customer Customer { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var appointment = (Appointment)validationContext.ObjectInstance;
			var appointmentContext = (AppointmentContext)validationContext.GetService(typeof(AppointmentContext));

			// Find appointments with the same StartTime but different CustomerId
			var conflictingAppointments = appointmentContext.Appointments
				.Where(a => a.StartTime == appointment.StartTime && a.CustomerId != appointment.CustomerId && a.Id != appointment.Id)
				.ToList();

			// Check if there are any conflicting appointments
			if (conflictingAppointments.Any())
			{
				yield return new ValidationResult("Conflicting appointments found");
			}
		}


	}
}
 