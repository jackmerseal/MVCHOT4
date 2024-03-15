using Microsoft.EntityFrameworkCore;
namespace MVCHOT4.Models
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions<AppointmentContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Username = "Matt Turk",
                    PhoneNumber = "123-456-7890"
                });

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    StartTime = new DateTime(2024, 03, 15, 08, 00, 00),
                    CustomerId = 1
                });
        }
    }
}
