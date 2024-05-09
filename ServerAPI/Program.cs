using Infrastructure.Repositories;
using ServerAPI.Repositories.Interfaces;
using ServerAPI.Repositories;
using Core.Interfaces;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IRegistrationRepository, RegistrationRepository>();

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

            app.UseCors("policy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
