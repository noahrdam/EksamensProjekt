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
        List<Event> GetAllEvents();
		void UpdateStatus(Application application);
        void DeleteApplication(int id);
        List<YouthVolunteer> GetAllYouthVolunteers();

        List<Application> GetFilteredApplicationsByWeek(int week);

        List<Application> GetFilteredApplicationsByPeriod(int week, string from, string to);


    }
}
