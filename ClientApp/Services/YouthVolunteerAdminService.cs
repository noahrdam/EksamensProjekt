using Core.Model;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public class YouthVolunteerAdminService : IYouthVolunteerAdminService
    {
        HttpClient http;
        private string _serverUrl = "https://localhost:7095";

        public YouthVolunteerAdminService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<List<YouthVolunteer>> GetAllYouthVolunteers()
        {
            var youthVolunteers = await http.GetFromJsonAsync<List<YouthVolunteer>>($"{_serverUrl}/api/aps/getallyouthvolunteers");
            return youthVolunteers;
        }
    }
}
