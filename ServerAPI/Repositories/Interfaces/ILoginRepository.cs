using Core.Model;

namespace ServerAPI.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Admin? GetAdmin(string username);
        List<Admin> GetAllAdmins();
        public bool VerifyLogin(string username, string password);
    }
}
