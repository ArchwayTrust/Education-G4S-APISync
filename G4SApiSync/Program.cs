using G4SApiSync.Client;
using G4SApiSync.Data;
using G4SApiSync.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G4SApiSync
{
    class Program
    {
        private static List<AcademySecurity> _AcademyKeys;
        static void Main(string[] args)
        {
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

    }
}
