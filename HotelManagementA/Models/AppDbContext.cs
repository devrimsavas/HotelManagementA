using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models.CustomerModels;
using HotelManagementA.Models.HotelModels;
using HotelManagementA.Models.InvoiceModels;
using HotelManagementA.Models.PaymentModels;
using HotelManagementA.Models.StaffModels;



namespace HotelManagementA.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Customer
        public DbSet<Customer> Customers { get; set; }

        // Hotel-related
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<HotelReview> HotelReviews { get; set; }

        // Staff
        public DbSet<Employee> Employees { get; set; }

        // Payment & Invoice
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Hotel ↔ Location (one-to-one)
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Location)
                .WithOne()
                .HasForeignKey<Hotel>(h => h.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hotel ↔ Employee (one-to-many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Hotel)
                .WithMany(h => h.Employees)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hotel ↔ Room (one-to-many)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Room ↔ RoomType (one-to-many)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Hotel ↔ HotelReview (one-to-many)
            modelBuilder.Entity<HotelReview>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // HotelReview ↔ Customer (many-to-one)
            modelBuilder.Entity<HotelReview>()
                .HasOne(r => r.Customer)
                .WithMany() // Customer’da Review listesi yok
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reservation relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Hotel)
                .WithMany()
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment ↔ Reservation (one-to-one)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne()
                .HasForeignKey<Payment>(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment ↔ Customer (many-to-one)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice ↔ Payment (one-to-one)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Payment)
                .WithOne()
                .HasForeignKey<Invoice>(i => i.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invoice ↔ Customer (many-to-one)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);





        }


    }
}
