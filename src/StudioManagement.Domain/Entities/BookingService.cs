using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioManagement.Domain.Entities
{
    [Table("Booking_Service")]
    public class BookingService
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }          // snapshot giá tại thời điểm booking

        // Navigation
        public Booking Booking { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}
