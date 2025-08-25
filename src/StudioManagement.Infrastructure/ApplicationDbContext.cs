using Microsoft.EntityFrameworkCore;
using StudioManagement.Domain.Entities;

namespace StudioManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingService> BookingServices => Set<BookingService>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== User - Role (1:N) =====
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== Room - Booking (1:N) =====
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>(e =>
            {
                e.Property(r => r.RoomStatus)
                 .HasConversion<string>()                 // enum <-> string trong DB
                 .HasMaxLength(20)
                 .HasColumnType("nvarchar(50)")
                 .HasDefaultValue(RoomStatus.Available);  // default ở DB
            });

            // ===== Booking - Payment (1:1) =====
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.BookingId)
                .IsUnique();

            // ===== Booking - Service (N:N qua BookingService) =====
            modelBuilder.Entity<BookingService>(bs =>
            {
                bs.HasKey(x => new { x.BookingId, x.ServiceId });

                bs.HasOne(x => x.Booking)
                  .WithMany(b => b.BookingServices)
                  .HasForeignKey(x => x.BookingId)
                  .OnDelete(DeleteBehavior.Cascade);

                bs.HasOne(x => x.Service)
                  .WithMany(s => s.BookingServices)
                  .HasForeignKey(x => x.ServiceId)
                  .OnDelete(DeleteBehavior.Restrict);

                bs.Property(x => x.Price).HasColumnType("decimal(10,2)");
            });

            // ===== Feedback =====
            modelBuilder.Entity<Feedback>(fb =>
            {
                fb.HasOne(f => f.Booking)
                  .WithMany(b => b.Feedbacks)
                  .HasForeignKey(f => f.BookingId)
                  .OnDelete(DeleteBehavior.Cascade);

                fb.HasOne(f => f.User)
                  .WithMany(u => u.Feedbacks)
                  .HasForeignKey(f => f.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            // ===== Kiểu tiền tệ =====
            modelBuilder.Entity<Room>().Property(r => r.RoomPrice).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Service>().Property(s => s.ServicePrice).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Booking>().Property(b => b.TotalPrice).HasColumnType("decimal(12,2)");
            modelBuilder.Entity<Booking>().Property(b => b.RoomPrice).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasColumnType("decimal(12,2)");

            //modelBuilder.Entity<Role>()
            //    .HasData(
            //        new Role { RoleId = 1, UserRole = "Admin" }
            //    );
            //modelBuilder.Entity<User>()
            //    .HasData(
            //        new User
            //        {
            //            UserId = 1,
            //            UserName = "admin",
            //            PasswordHash = "1", // ⚠ Thực tế nên hash
            //            Email = "admin@example.com",
            //            FullName = "Administrator",
            //            Phone = "0123456789",
            //            RoleId = 1
            //        }
            //    );
            base.OnModelCreating(modelBuilder);
        }
    }
}
