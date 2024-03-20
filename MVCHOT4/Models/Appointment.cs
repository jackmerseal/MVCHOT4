using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCHOT4.Models
{
    public class Appointment : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        [FutureDate(ErrorMessage = "Appointment must be in the future")]
        [IsTimeSlotAvailable(ErrorMessage = "This appointment time is not available")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }
        [ValidateNever]
        public Customer? Customer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var appointmentContext = (AppointmentContext)validationContext.GetService(typeof(AppointmentContext));
            var appointments = appointmentContext.Appointments.Where(a => a.StartTime ==  StartTime).ToList();

            if(appointments.Any())
            {
                var isTimeSlotAvailableAttribute = validationContext.ObjectType
                    .GetCustomAttributes(typeof(IsTimeSlotAvailableAttribute), true)
                    .FirstOrDefault() as IsTimeSlotAvailableAttribute;

                if(isTimeSlotAvailableAttribute != null)
                {
                    yield return new ValidationResult(isTimeSlotAvailableAttribute.ErrorMessage);
                }
            }
        }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime = (DateTime)value;
            if(dateTime < DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

    public class IsTimeSlotAvailableAttribute : ValidationAttribute
    { 
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var appointment = (Appointment)validationContext.ObjectInstance;
            var appointmentContext = (AppointmentContext)validationContext.GetService(typeof(AppointmentContext));

            var appointments = appointmentContext.Appointments
                .Where(a => a.StartTime == appointment.StartTime && a.Id != appointment.Id && a.CustomerId != 0)
                .ToList();

            if(appointments.Any())
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
 