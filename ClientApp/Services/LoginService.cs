using System.Net.Http.Json;
using Core.Model;

namespace ClientApp.Services
{
    public class LoginService : ILoginService
    {
        HttpClient http;
        private string _serverUrl = "https://localhost:7095";

        public LoginService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<bool> VerifyLogin(string username, string password)
        {
            var response = await http.GetFromJsonAsync<bool>($"{_serverUrl}/api/admins/verify?username={username}&password={password}");
            return response;
        }

        public async Task<Admin> GetAdmin(string username)
        {
            var admin = await http.GetFromJsonAsync<Admin>($"{_serverUrl}/api/admins/getadmin?username={username}");
            return admin;
        }

    }
}
