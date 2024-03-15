using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVCHOT4.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }
        [ValidateNever]
        public Customer Customer { get; set; } = null!;
    }
}
