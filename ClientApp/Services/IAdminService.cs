using Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IAdminService
    {
        Task<List<Application>> GetAllApplications();
        Task<List<Event>> GetAllEvents();
        Task<List<Application>> GetFilteredApplicationsByWeek(int week);
        Task<List<Application>> GetFilteredApplicationsByPeriod(int week, string from, string to);
        Task<bool> UpdateStatus(Application application);
        Task<bool> DeleteApplication(int applicationId);
        Task<bool> PublishAllApplications(List<Application> applications);
    }
}
