using System.Text.Json.Serialization;

namespace StudioManagement.Contract.DTO.Request
{
    public class ServiceRequest
    {
        [JsonPropertyName("serviceName")]
        public string ServiceName { get; set; }
        [JsonPropertyName("servicePrice")]
        public decimal ServicePrice { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
