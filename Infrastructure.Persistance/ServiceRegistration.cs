
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Infrastructure.Persistance.Context;
using Infrastructure.Persistance.Repository;
using Infrastructure.Persistance.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
        }
    }
}
