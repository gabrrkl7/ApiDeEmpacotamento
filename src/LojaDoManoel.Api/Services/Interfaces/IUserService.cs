using LojaDoManoel.Api.Models;

namespace LojaDoManoel.Api.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
    }
}
