using System.Text.Json.Serialization;

namespace StudioManagement.Contract.DTO.Request
{
    public class RegisterRequest
    {
        [JsonPropertyName("fullname")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("username")]
        public string UserName { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;
    }
}
