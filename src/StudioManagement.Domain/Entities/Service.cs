using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioManagement.Domain.Entities
{
    [Table("Services")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required, MaxLength(100)]
        public string ServiceName { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal ServicePrice { get; set; }   // giá gốc của dịch vụ

        // N–N với Booking qua bảng trung gian
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
