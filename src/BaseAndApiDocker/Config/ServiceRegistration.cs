using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Authenticate;
using System.Text;

namespace BaseAndApiDocker.Config
{
    public class ServiceRegistration
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddAuthentication(authOption =>
            {
                authOption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOption => {
                var key = configuration.GetValue<string>("JwtConfig:Key");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                jwtOption.SaveToken = true;
                jwtOption.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
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
            });

        }
    }
}
