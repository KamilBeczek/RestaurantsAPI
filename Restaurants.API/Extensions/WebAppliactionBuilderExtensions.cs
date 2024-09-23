using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication();
            // Add services to the container.
            builder.Services.AddControllers();

            // Tworzenie w prawym górnym rogu przycisku Authorize i przekazywanie tokena przechowanego w nim do każdego zapytania API
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme, 
                                Id = "bearerAuth" 
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<ErrorHandlingMiddle>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        }
    }
}