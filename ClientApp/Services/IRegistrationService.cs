using System;
using System.Threading.Tasks;
using Core.Model;
namespace ClientApp.Services
{
    public interface IRegistrationService
    {
        Task<List<Event>> GetAllEvents();
        Task<bool> SaveApplication(Application application);
        Task<bool> SaveYouthApplication(YouthVolunteer youthVolunteer);
    }
}
