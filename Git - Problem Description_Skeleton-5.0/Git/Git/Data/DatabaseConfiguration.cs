using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Data
{
    public static class DatabaseConfiguration
    {
        static DatabaseConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddUserSecrets<Startup>()
               .Build();

            ConnectionString = config.GetConnectionString("Git");
        }

        public static string ConnectionString { get; }
    }
}
