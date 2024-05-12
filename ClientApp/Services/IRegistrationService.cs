using System;
using System.Threading.Tasks;
using Core.Model;
namespace ClientApp.Services
{
    public interface IRegistrationService
    {
        Task<bool> RegisterApplication(Application application);

        Task<List<Event>> GetEvents();
    }
}
