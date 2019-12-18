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
        private static List<AcademySecurity> _academyList;
        private static G4SContext _context;
        private static string _connectionString;

        public static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            _academyList = _context.AcademySecurity.ToList();

            foreach(var academy in _academyList)
            {
                Console.WriteLine(academy.AcademyCode + Environment.NewLine);
            }

            //Run main sync code
            RunApiSync().Wait();
        }

        static private async Task RunApiSync()
        {
            Console.WriteLine("Running API Sync. This will take some time." + Environment.NewLine);

            foreach (var academy in _academyList)
            {
                bool result;
                var GetData = new GetAndStoreAllData(_context, _connectionString, academy);

                result = await GetData.RunStudents();

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
            //services.AddSingleton<IConfigurationRoot>(configuration);

            _connectionString = configuration.GetConnectionString("G4SContext");

            services.AddDbContext<G4SContext>(options =>
                    options.UseSqlServer(_connectionString));

            var serviceProvider = services.BuildServiceProvider();
            _context = serviceProvider.GetService<G4SContext>();
        }
    }
}
