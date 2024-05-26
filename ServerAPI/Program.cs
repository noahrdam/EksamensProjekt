using ServerAPI.Repositories;
using ServerAPI.Repositories.Interfaces;
using ServerAPI.EmailCommunication;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSingleton<ILoginRepository, LoginRepository>();
            builder.Services.AddSingleton<IAdminRepository, AdminRepository>();
            builder.Services.AddSingleton<IRegistrationRepository, RegistrationRepository>();
			builder.Services.AddSingleton<IEmailService, EmailService>();



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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("policy");

            app.MapControllers();

            app.Run();

        } 

    }
}