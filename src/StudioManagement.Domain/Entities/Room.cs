using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudioManagement.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoomStatus
    {
        Available,
        Occupied,
        Maintenance,
        Inactive,
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

        public Room() { } // constructor mặc định cho EF Core

        public Room(string roomName, int quantity, decimal roomPrice)
        {
            RoomName = roomName;
            Quantity = quantity;
            RoomPrice = roomPrice;
        }
    }
}
