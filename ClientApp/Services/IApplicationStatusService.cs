using Core.Model;
namespace ClientApp.Services
{
    public interface IApplicationStatusService
    {
        Task<List<Application>> GetAllApplications();
    }
}
