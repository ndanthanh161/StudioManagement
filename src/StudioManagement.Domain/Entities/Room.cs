using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioManagement.Domain.Entities
{
    public enum RoomStatus
    {
        Available = 0,
        Occupied = 1,
        Maintenance = 2,
        Inactive = 3
    }

    [Table("Rooms")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [Required, MaxLength(100)]
        public string RoomName { get; set; } = null!;

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public decimal RoomPrice { get; set; }

        [Required]
        public RoomStatus RoomStatus { get; set; } = RoomStatus.Available;

        // Nếu có bảng Booking tham chiếu RoomId:
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
