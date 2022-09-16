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

            //Check for command line arguments. If not ATT then run full sync.
            if (args.Length > 0)
            {
                if (args[0] == "ATT")
                {
                    RunApiAttendanceSync().Wait();
                }

                else if (args[0] == "BEH")
                {
                    RunApiBehaviourSync().Wait();
                }
            }
            else
            {
                    RunApiSync().Wait();
            }
            
        }

        static private async Task RunApiSync()
        {
            Console.WriteLine("Running API Full Sync. This will take some time." + Environment.NewLine);
            var GetData = new GetAndStoreAllData(_context, _connectionString);

            //Sync Student end points.
            var results = await GetData.SyncStudents();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync Teaching end points.
            results = await GetData.SyncTeaching();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync Assessment end points.
            results = await GetData.SyncAssessment();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync Attainment end points.
            results = await GetData.SyncAttainment();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync Attendance end points.
            results = await GetData.SyncAttendance();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync Timetable end points.
            results = await GetData.SyncTimetable();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync Behaviour end points.
            results = await GetData.SyncBehaviour();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync User end points.
            results = await GetData.SyncUsers();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

        }

        static private async Task RunApiAttendanceSync()
        {
            Console.WriteLine("Running API Attendance Sync. This will take some time." + Environment.NewLine);
            var GetData = new GetAndStoreAllData(_context, _connectionString);

            //Sync Attendance end points.
            var results = await GetData.SyncAttendance();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

        }

        static private async Task RunApiBehaviourSync()
        {
            Console.WriteLine("Running API Behaviour Sync. This will take some time." + Environment.NewLine);
            var GetData = new GetAndStoreAllData(_context, _connectionString);

            //Sync Attendance end points.
            var results = await GetData.SyncAttendance();

            foreach (var result in results)
            {
                Console.WriteLine(result.LoggedAt + " " + result.AcademyCode + " - " + result.EndPoint + " - " + result.Result);
            }

            //Sync Behaviour end points.
            results = await GetData.SyncBehaviour();

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
            _context.Database.SetCommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds);

            _context.Database.Migrate();
        }
    }
}
