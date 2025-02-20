using Core.Application.Request;
using Core.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddScoped<IValidator<UserCreateRequest>, CreateUserValidator>();

           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration["Jwt:Issuer"],
                   ValidAudience = configuration["Jwt:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(key)
               };
           });

           services.AddAuthorization();
        }
    }
}
