using System;
using Microsoft.Extensions.DependencyInjection;
using ProjectStructureDemo.IRepository;
using ProjectStructureDemo.IServices;
using ProjectStructureDemo.Repository;
using ProjectStructureDemo.Services;
namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServiceProvider(this IServiceCollection services,string connectionStr)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //services.AddScoped<DBSeed>();
            //services.AddScoped<MyContext>();
            services.AddScoped<IUserRepository>(repo=>new UserRepository(connectionStr));
            services.AddScoped<IUserService, UserService>();
        }
    }
}
