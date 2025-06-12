using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Toshi.Backend.Application.Models.Identity;
using Toshi.Backend.Domain.DTO.Common;
using Toshi.Backend.Infraestructure.Persistence.Data;
using Toshi.Backend.Infraestructure.Services;

namespace Toshi.Backend.Infraestructure
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<StringSecurity>(configuration.GetSection("StringSecurity"));

            services.AddDbContext<ToshiDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ToshiDBContext")!)
            );

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(c =>
                {
                    c.RequireHttpsMetadata = false;
                    c.SaveToken = true;
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),

                        ClockSkew = TimeSpan.FromDays(1),
                    };
                });

            var securitySettings = configuration.GetSection("StringSecurity").Get<StringSecurity>();

            //Constants.CRYPTO_KEY = securitySettings.Key;
            //Constants.CRYPTO_VECTOR = securitySettings.Vector;

            services.AddSingleton<EncryptionService>();

            return services;
        }
    }
}
