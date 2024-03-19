using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

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
        public Customer? Customer { get; set; }
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
            DateTime dateTime = (DateTime)value;
            Appointment appointment = (Appointment)validationContext.ObjectInstance;

            List<Appointment> appointments = GetAppointmentsFromDatabase(appointmentContext)

            if(appointments.Any(a => a.StartTime == dateTime))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }

        private List<Appointment> GetAppointmentsFromDatabase(AppointmentContext appointmentContext)
        {
            return AppointmentContext.Appointments.Where(a => a.StartTime == DateTime.Now).ToList();
            return new List<Appointment>();
        }
    }

}
 