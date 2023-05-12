using Conference.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public static class ConferenceDbContextExtensions
    {
        public static IServiceCollection AddConferenceDbContext(
            this IServiceCollection services,
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ConferenceDemo")

        {
            services.AddDbContext<ConferenceDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

                //options.LogTo(Console.WriteLine,
                //  new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
            });


            return services;
        }
        public static void EnsureSeeded(this ConferenceDbContext context)
        {
            DataSeeder.SeedData(context);
        }
    }
}
