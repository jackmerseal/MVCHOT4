namespace MVCHOT4.Models
{
    public class AppointmentViewModel
    {
        public Appointment Appointment { get; set; }
        public Customer Customer { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Customer> Customers { get; set; }
        public AppointmentViewModel()
        {
            Appointments = new List<Appointment>();
            Customers = new List<Customer>();
        }
    }
}
