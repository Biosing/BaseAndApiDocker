using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Utils;
using Services.Authenticate;
using Services.Docs;
using System.Text;

namespace BaseAndApiDocker.Config
{
    public class ServiceRegistration
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddDistributedMemoryCache();

            services.AddTransient<IHttpContext, AppHttpContext>();
            
            services.AddScoped<ICache, ApplicationCache>();
            services.AddScoped<IJwtCacheStorage, JwtCacheStorage>();

            services.AddScoped<IAuthorization, Authorization>();

            services
                .AddScoped<IJwtTokenService, JwtTokenService>()
                .AddScoped<IDocService, DocService>();


            services.AddAuthentication(authOption =>
            {
                authOption.DefaultAuthenticateScheme = "Bearer";
                authOption.DefaultChallengeScheme = "OpenIdConnect";
            }).AddJwtBearer("Bearer", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new JwtSecretKey(new string(configuration["Authentication:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => {
                options.CustomSchemaIds(type => type.ToString());
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "jwtToken_Auth_API",
                    Version = "v1",
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Here Enter JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                options.OperationFilter<SwaggerFileOperationFilter>();
            });

        }
    }
}
