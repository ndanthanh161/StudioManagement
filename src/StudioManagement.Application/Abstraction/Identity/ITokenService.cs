namespace StudioManagement.Application.Abstraction.Identity
{
    public interface ITokenService
    {
        (string Token, DateTime ExpiredAtUtc) CreateToken(string UserName, string Email, string FullName, string Role);
    }
}
