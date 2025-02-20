
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Infrastructure.Persistance.Context;
using Infrastructure.Persistance.Repository;
using Infrastructure.Persistance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Infrastructure.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceInfrastructure(this IServiceCollection svc, IConfiguration config)
        {
            svc.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("EvaluaciconUniversal"));


            svc.AddAutoMapper(Assembly.GetExecutingAssembly());

            svc.AddTransient<IUserService, UserService>();

            svc.AddTransient<IJwtService, JwtService>();

            svc.AddHttpClient<IJsonPlaceHolderService, JsonPlaceHolderService>();

            svc.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            svc.AddTransient<IUserRepository, UserRepository>();

            svc.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            svc.AddAuthorization();
        }
    }
}
