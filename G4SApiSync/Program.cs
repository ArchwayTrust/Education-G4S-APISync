using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace G4SApiSync
{
    class Program
    {
        public static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            //Run main sync code
            RunApiSync();
        }

        public static void RunApiSync()
        {
            Console.WriteLine(configuration.GetConnectionString("G4SContext"));
            Console.WriteLine("Hello World!");
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            
            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        }

    }
}
