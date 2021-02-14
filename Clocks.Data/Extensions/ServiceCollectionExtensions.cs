using System;
using Clocks.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clocks.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection,
            string connectionString)
        {
            serviceCollection.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            serviceCollection.AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.User.RequireUniqueEmail = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            return serviceCollection;
        }
    }
}
