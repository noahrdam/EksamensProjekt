using System.Net.Http.Json;
using Core.Model;

namespace ClientApp.Services
{
    public class RegistrationService: IRegistrationService
    {
        HttpClient http;

        private string serverUrl = "https://localhost:7095";

        private RegistrationService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<bool> RegisterApplication(Application application)
        {
            var response = await http.PostAsJsonAsync($"{serverUrl}/api/registration", application);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Event>> GetEvents()
        {
            var events = await http.GetFromJsonAsync<List<Event>>($"{serverUrl}/api/registration/getallevents");
            return events;
        }


    }
}
