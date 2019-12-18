using G4SApiSync.Client;
using G4SApiSync.Data;
using G4SApiSync.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace G4SApiSync
{
    class Program
    {
        private static List<AcademySecurity> _AcademyKeys;
        public static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            using (G4SContext context = new G4SContext())
            {
                _AcademyKeys = context.AcademySecurity.ToList();
            }

            //Run main sync code
            RunApiSync().Wait();
        }

        static private async Task RunApiSync()
        {
            string StatusMessage = "Running API Sync. This will take some time." + Environment.NewLine;
            Console.WriteLine(StatusMessage);

            foreach (var academy in _AcademyKeys)
            {
                var GetData = new GetAndStoreAllData(academy.APIKey, academy.AcademyCode, academy.CurrentAcademicYear, StatusMessage);

                StatusMessage = await GetData.RunStudents();

                //StatusMessage = await GetData.RunTeaching();
                //tb1.Text = StatusMessage;
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            services.AddSingleton<IConfigurationRoot>(configuration);

            services.AddDbContext<G4SContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("G4SContext")));
        }

    }
}
