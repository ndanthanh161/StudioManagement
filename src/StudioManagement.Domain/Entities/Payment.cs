using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioManagement.Domain.Entities
{
    public enum PaymentStatus
    {
        PENDING = 0,
        PAID = 1,
        REFUNDED = 2
    }
    [Table("Payments")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        // FK -> Booking (1–1)
        [Required]
        public int BookingId { get; set; }

        [Required]
        public decimal Amount { get; set; } // sẽ set precision bằng Fluent API

        [MaxLength(50)]
        public string? PaymentMethod { get; set; } // CASH, CARD, BANK, WALLET...

        [Required, MaxLength(20)]
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string? Note { get; set; }

        // Navigation property
        public Booking Booking { get; set; } = null!;
    }
}
