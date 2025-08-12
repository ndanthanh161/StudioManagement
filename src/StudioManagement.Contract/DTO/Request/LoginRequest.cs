using System.Text.Json.Serialization;

namespace StudioManagement.Contract.DTO.Request
{
    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
