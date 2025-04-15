using ProjetoColeta.Models;

namespace ProjetoColeta.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(string username, string password);
    }
}