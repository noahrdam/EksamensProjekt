// IAdminRepository.cs
using Core.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        List<Application> GetAllApplication();
        List<Application> GetFilteredApplications(FilterDefinition<Application> filter);
        List<Event> GetAllEvents();
        Task UpdateApplication(Application application);

		Task UpdateStatus(Application application);
        void DeleteApplication(int id);
        List<YouthVolunteer> GetAllYouthVolunteers();
        Application GetApplicationById(int id);
    }
}
