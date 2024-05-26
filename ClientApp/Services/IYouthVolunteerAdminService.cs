using Core.Model;
namespace ClientApp.Services
{
    public interface IYouthVolunteerAdminService
    {
        Task<List<YouthVolunteer>> GetAllYouthVolunteers();
    }
}
