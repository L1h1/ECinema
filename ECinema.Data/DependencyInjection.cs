using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ECinema.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace ECinema.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services,IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("RemoteSQLConnection");
            services.AddScoped<MySqlConnection>(_ => new MySqlConnection(connString));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
