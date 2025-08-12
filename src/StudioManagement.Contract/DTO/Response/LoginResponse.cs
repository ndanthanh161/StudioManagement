namespace StudioManagement.Contract.DTO.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAtUtc { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

    }
}
