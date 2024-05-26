using ClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

namespace ClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<IRegistrationService, RegistrationService>();

            builder.Services.AddSingleton<IAdminService, AdminService>();

            builder.Services.AddSingleton<ILoginService, LoginService>();

            builder.Services.AddSingleton<IYouthVolunteerAdminService, YouthVolunteerAdminService>();

            builder.Services.AddSingleton<IApplicationStatusService,ApplicationStatusService>();

            builder.Services.AddBlazoredLocalStorage();


            await builder.Build().RunAsync();
        }
    }
}
