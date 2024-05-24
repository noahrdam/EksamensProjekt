using Core.Model;

namespace ServerAPI.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Admin? GetAdmin(string username);
        public bool VerifyLogin(string username, string password);
    }
}
