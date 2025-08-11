using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioManagement.Domain.Entities
{
    public enum BookingStatus
    {
        PENDING = 0,
        CONFIRMED = 1,
        COMPLETED = 2,
        CANCELLED = 3
    }
    [Table("Bookings")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; }

        // FK -> Rooms
        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        // Tổng tiền cuối (phòng + dịch vụ)
        [Required]
        [Precision(12, 2)]
        public decimal TotalPrice { get; set; }

        // Snapshot giá phòng tại thời điểm đặt
        [Required]
        [Precision(10, 2)]
        public decimal RoomPrice { get; set; }

        // PENDING / CONFIRMED / COMPLETED / CANCELLED
        [Required, MaxLength(20)]
        public BookingStatus BookingStatus { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Room Room { get; set; } = null!;
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

        // 1–1: mỗi booking chỉ 1 payment
        public Payment? Payment { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    }
}
