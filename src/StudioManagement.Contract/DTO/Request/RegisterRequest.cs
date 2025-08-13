using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        [JsonPropertyName("role")]
        public string Role { get; set; } = "CUSTOMER";
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;
    }
}
