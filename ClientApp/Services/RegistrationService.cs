using System.Net.Http.Json;
using Core.Model;

namespace ClientApp.Services
{
    public class RegistrationService : IRegistrationService
    {
        HttpClient http;
        private string _serverUrl = "https://localhost:7095";

        public RegistrationService(HttpClient http)
        {
           this.http = http;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            var events = await http.GetFromJsonAsync<List<Event>>($"{_serverUrl}/api/registration/getallevents");
            return events;
        }

        public async Task<bool> SaveApplication(Application application)
        {
            var response = await http.PostAsJsonAsync($"{_serverUrl}/api/registration/register", application);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SaveYouthApplication(YouthVolunteer youthVolunteer)
        {
            var response = await http.PostAsJsonAsync($"{_serverUrl}/api/registration/registerYouthVolunteer", youthVolunteer);
            return response.IsSuccessStatusCode;
        }
    }
}
