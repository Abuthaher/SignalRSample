using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SampleNotifiaction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNotifiaction.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString) where T : IDatabaseSubscription
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetService<T>();
            subscription.Configure(connectionString);
        }
    }
}
