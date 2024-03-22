using System.ComponentModel.DataAnnotations;

namespace MVCHOT4.Models
{
	public class IsTimeSlotAvailableAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var appointment = (Appointment)validationContext.ObjectInstance;
			if (appointment == null)
			{
				return new ValidationResult("Appointment is null");
			}
			var appointmentContext = (AppointmentContext)validationContext.GetService(typeof(AppointmentContext));

			var appointments = appointmentContext.Appointments
				.Where(a => a.StartTime == appointment.StartTime)
				.ToList();

			appointments = appointments.Where(a => a.Id != appointment.Id).ToList();

			DateTime startTime = (DateTime)value;

			if (appointments.Any())
			{
				if (startTime == appointment.StartTime)
				{
					return new ValidationResult(ErrorMessage);
				}
			}

			return ValidationResult.Success;
		}
	}
}
