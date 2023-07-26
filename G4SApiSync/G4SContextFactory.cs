using G4SApiSync.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace G4SApiSync
{
    //This is only used during database creation/migration.
    public class G4SContextFactory : IDesignTimeDbContextFactory<G4SContext>
    {
        public G4SContext CreateDbContext(string[] args)
        {
            //Set configuration to point at appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            //Set options for G4SContext
            var optionsBuilder = new DbContextOptionsBuilder<G4SContext>();
            optionsBuilder.UseSqlServer(
                   configuration.GetConnectionString("G4SContext"),
                   opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds)
            );

            return new G4SContext(optionsBuilder.Options);
        }
    }
}
