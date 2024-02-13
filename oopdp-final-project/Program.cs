using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using oopdp_final_project.DatabaseConnection;

var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

var connectionString = configuration.GetConnectionString("DatabaseContext");

var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
optionsBuilder.UseSqlServer(connectionString);

using (var context = new DatabaseContext(optionsBuilder.Options))
{
    // Your code here to work with the database
}