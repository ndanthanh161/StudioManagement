namespace StudioManagement.Contract.DTO.Response
{

    public sealed class RoomResponse
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Quantity { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomStatus { get; set; }
    }

}
