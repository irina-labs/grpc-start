using Data.Interface;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public static class DataExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddConferenceDbContext();
            services.AddTransient<ISpeakerRepository, SpeakerRepository>();
            services.AddTransient<ITalkRepository, TalkRepository>();
            return services;
        }
    }
}
