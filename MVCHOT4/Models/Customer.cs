using System.ComponentModel.DataAnnotations;

namespace MVCHOT4.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
    }
}
