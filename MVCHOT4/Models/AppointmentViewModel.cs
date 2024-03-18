namespace MVCHOT4.Models
{
    public class AppointmentViewModel : Appointment
    {
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public Appointment Appointment { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartTime { get; set; }
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public AppointmentViewModel()
		{
			Appointments = new List<Appointment>();
			Customers = new List<Customer>();
		}
	}
}
