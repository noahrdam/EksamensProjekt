using ServerAPI.Repositories.Interfaces;
using ServerAPI.Repositories;

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();


        }


    }
}