using LojaDoManoel.Api.Models.Auth;

namespace LojaDoManoel.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(string username, string password);
        Task CreateAdminUserAsync();
    }
}