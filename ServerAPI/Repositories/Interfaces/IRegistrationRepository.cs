using Core.Model;

namespace ServerAPI.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        void RegisterApplication(Application application);

        List<Event> GetEvents();

        void AddVolunteer(Volunteer volunteer);

        void AddYouthVolunteer(YouthVolunteer youthVolunteer);
    }
}
