using G4SApiSync.Client;
using G4SApiSync.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace G4SApiSync
{
    class Program
    {
        private static G4SContext _context;
        private static string _connectionString;

        public static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            //Run main sync code
            RunApiSync().Wait();
        }

        static private async Task RunApiSync()
        {
            Console.WriteLine("Running API Sync. This will take some time." + Environment.NewLine);
            var GetData = new GetAndStoreAllData(_context, _connectionString);
            var results = await GetData.SyncStudents();

            foreach(var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            results = await GetData.SyncTeaching();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            results = await GetData.SyncAssessment();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
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

            _context.Database.Migrate();
        }
    }
}
