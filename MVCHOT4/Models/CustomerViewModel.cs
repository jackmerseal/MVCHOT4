namespace MVCHOT4.Models
{
	public class CustomerViewModel : Customer
	{
		public IEnumerable<Appointment> Appointments { get; set; }
		public IEnumerable<Customer> Customers { get; set; }
		public Appointment Appointment { get; set; }
		public Customer Customer { get; set; }
		public string Username { get; set; }
		public int Id { get; set; }
		public int CustomerId { get; set; }

		public CustomerViewModel()
		{
			Appointments = new List<Appointment>();
			Customers = new List<Customer>();
		}
	}
}
