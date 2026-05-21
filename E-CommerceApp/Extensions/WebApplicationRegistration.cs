using Domain.Contracts;
using E_CommerceApp.Middelwares;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace E_CommerceApp.Extensions
{
    public static class WebApplicationRegistration
    {
        #region Data Seeding
        public static async Task SeedDataAsync(this WebApplication app)
        {
            // Seed the data into the database
            var Scope = app.Services.CreateScope();
            // Get the IDataSeeding service and call the SeedData method to seed the data
            var Seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            // Call the SeedData method to seed the data into the database
            await Seed.DataSeedAsync();
            // Call the SeedData method to seed the Identity data into the database
            await Seed.IdentityDataSeedAsync();

        }
        #endregion;

        #region Handling Exceptions
        public static IApplicationBuilder UseCustomExceptionMiddelware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalErrorHandlingMiddelware>();
        }
        #endregion

        // helper method to use the swagger middlewares in the development environment
        #region configer swagger
        public static IApplicationBuilder UseSwaggerMiddelwares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "E-Commerce API"; // Title of the Swagger UI page

                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true  // Configure the Swagger UI to display the duration of each API request in the UI
                };

                options.JsonSerializerOptions = new JsonSerializerOptions()  // Configure JSON serialization options
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase  // Configure the JSON serializer to use camelCase naming convention for property names in the generated Swagger documentation
                };

                options.DocExpansion(DocExpansion.None); // Set the default expansion state of the API documentation to "None" (collapsed)

                options.EnableFilter(); // Enable the search/filter box in the Swagger UI to allow users to filter the displayed API endpoints based on their input

                options.EnablePersistAuthorization(); // Enable the "Persist Authorization" feature in the Swagger UI, which allows users to save their authorization credentials (e.g., API keys, tokens) across sessions, so they don't have to re-enter them every time they access the API documentation
            }
            );

            return app;
        } 
        #endregion

    }
}
