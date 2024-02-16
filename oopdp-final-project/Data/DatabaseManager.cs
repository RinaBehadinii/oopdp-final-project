using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.DatabaseConnection
{
    public class DatabaseManager
    {
        private static readonly object _lock = new object();
        private static DatabaseContext _instance;

        public static DatabaseContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                            IConfiguration configuration = builder.Build();

                            var connectionString = configuration.GetConnectionString("DatabaseContext");
                            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                            optionsBuilder.UseSqlServer(connectionString);

                            _instance = new DatabaseContext(optionsBuilder.Options);
                        }
                    }
                }
                return _instance;
            }
        }

        private DatabaseManager() { }
    }
}
