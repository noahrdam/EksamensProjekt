using ServerAPI.Repositories;
using ServerAPI.Repositories.Interfaces;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IAdminRepository, AdminRepository>();

            builder.Services.AddSingleton<IRegistrationRepository, RegistrationRepository>();
            builder.Services.AddSingleton<IAdminRepository, AdminRepository>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("policy",
                    policy =>
                    {
                        policy.AllowAnyOrigin();
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();


                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("policy");

            app.UseRouting();

            app.UseCors("policy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
