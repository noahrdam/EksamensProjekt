using Core.Model;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public class ApplicationStatusService : IApplicationStatusService
    {
        HttpClient http;
        private string _serverUrl = "https://localhost:7095";

        public ApplicationStatusService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<List<Application>> GetAllApplications()
        {
            var applications = await http.GetFromJsonAsync<List<Application>>($"{_serverUrl}/api/aps/getall");
            return applications;
        }
    }
}
