using Core.Model;

namespace ClientApp.Services
{
    public interface ILoginService
    {
        Task<bool> VerifyLogin(string username, string password);
        Task<Admin> GetAdmin(string username);


    }
}
