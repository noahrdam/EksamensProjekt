using Core.Model;
using System.Net.Http.Json;

namespace ClientApp.Services
{
    public class AdminService : IAdminService
    {
        HttpClient http;
        private string _serverUrl = "https://localhost:7095";

        public AdminService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<List<Application>> GetAllApplications()
        {
            return await http.GetFromJsonAsync<List<Application>>($"{_serverUrl}/api/aps/getall");
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await http.GetFromJsonAsync<List<Event>>($"{_serverUrl}/api/aps/getallevents");
        }

        public async Task<List<Application>> GetFilteredApplicationsByWeek(int week)
        {
            return await http.GetFromJsonAsync<List<Application>>($"{_serverUrl}/api/aps/getfilteredbyweek?week={week}");
        }

        public async Task<List<Application>> GetFilteredApplicationsByPeriod(int week, string from, string to)
        {
            return await http.GetFromJsonAsync<List<Application>>($"{_serverUrl}/api/aps/getfilteredbyperiod?week={week}&from={from}&to={to}");
        }

        public async Task<bool> UpdateStatus(Application application)
        {
            var response = await http.PutAsJsonAsync($"{_serverUrl}/api/aps/updatestatus", application);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteApplication(int applicationId)
        {
            var response = await http.DeleteAsync($"{_serverUrl}/api/aps/delete/{applicationId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PublishAllApplications(List<Application> applications)
        {
            var response = await http.PutAsJsonAsync($"{_serverUrl}/api/aps/publishall", applications);
            return response.IsSuccessStatusCode;
        }


		public async Task<bool> SendManyEmails(List<Application> applications)
		{
			var response = await http.PostAsJsonAsync($"{_serverUrl}/api/aps/sendmanyemails", applications);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> SendEmail(Email email)
		{
			var response = await http.PostAsJsonAsync($"{_serverUrl}/api/aps/sendemail", email);
			return response.IsSuccessStatusCode;
		}

	}
}
