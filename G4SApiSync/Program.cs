using G4SApiSync.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;


namespace G4SApiSync
{
    class Program
    {
        public static IConfiguration configuration;
        static void Main(string[] args)
        {
            //Run main sync code
            RunApiSync();
        }

        public static void RunApiSync()
        {
            Console.WriteLine(configuration.GetConnectionString("G4SContext"));
            Console.WriteLine("Hello World!");
        }

    }
}
