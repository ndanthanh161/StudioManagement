using System.Text.Json.Serialization;

namespace StudioManagement.Contract.DTO.Request
{
    public class RoomRequest
    {
        [JsonPropertyName("roomname")]
        public string RoomName { get; set; } = null!;
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("roomprice")]
        public decimal RoomPrice { get; set; }
    }
}
