using E_CommerceApp.Extensions;
using Persistence;
using Service;

namespace E_CommerceApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationsServices();
            builder.Services.AddJWTServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            // Seed First Data into the database
            await app.SeedDataAsync();

            #region Configure the HTTP request pipeline
            app.UseCustomExceptionMiddelware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddelwares();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
