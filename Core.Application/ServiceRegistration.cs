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
        public static void AddApplicationLayer(this IServiceCollection services)
        {
           services.AddAutoMapper(Assembly.GetExecutingAssembly());
           services.AddScoped<IValidator<UserCreateRequest>, CreateUserValidator>();

        }
    }
}
