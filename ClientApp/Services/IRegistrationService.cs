using Core.Model;
namespace ClientApp.Services
{
    public interface IRegistrationService
    {
		Task<List<Event>> GetAllEvents();

        Task<List<Application>> GetAllApplications();
        Task<bool> SaveApplication(Application application);
		Task<bool> SaveYouthApplication(YouthVolunteer youthVolunteer);
	}
}
