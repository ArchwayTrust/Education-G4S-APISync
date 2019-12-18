using G4SApiSync.Client.EndPoints;
using G4SApiSync.Data;
using G4SApiSync.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4SApiSync.Client
{
    public class GetAndStoreAllData
    {
        private readonly AcademySecurity _academy;
        private readonly G4SContext _context;
        private readonly string _connectionString;

        public GetAndStoreAllData(G4SContext context, string connectionString, AcademySecurity academy)
        {
            _academy = academy;
            _context = context;
            _connectionString = connectionString;
        }

        public async Task<bool> RunStudents()
        {
            
            bool Sucess;

            //GET Student Details

            GETStudentDetails StudentEndPoint = new GETStudentDetails(_context, _connectionString);

            Sucess = await StudentEndPoint.UpdateDatabase(_academy.APIKey, _academy.CurrentAcademicYear, _academy.AcademyCode);

            if (Sucess)
            {
                Console.WriteLine("Students endpoint suceeded" + Environment.NewLine);
            }
            else
            {
                Console.WriteLine("Students endpoint failed" + Environment.NewLine);
            }

            //GET Education Details

            //var EducationDetailsEndPoint = new GETEducationDetails();

            //Sucess = await EducationDetailsEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            //if (Sucess)
            //{
            //    StatusMessage = StatusMessage + "Education details endpoint suceeded." + Environment.NewLine;
            //}
            //else
            //{
            //    StatusMessage = StatusMessage + "Education details endpoint failed." + Environment.NewLine;
            //}

            //GET General Attributes

            //var GeneralAttributesEndPoint = new GETGeneralAttributes();

            //Sucess = await GeneralAttributesEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            //if (Sucess)
            //{
            //    StatusMessage = StatusMessage + "General attributes endpoint suceeded." + Environment.NewLine;
            //}
            //else
            //{
            //    StatusMessage = StatusMessage + "General attributes endpoint failed." + Environment.NewLine;
            //}

            ////GET Sensitive Attributes
            //var SensitiveAttributesEndPoint = new GETSensitiveAttributes();

            //Sucess = await SensitiveAttributesEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            //if (Sucess)
            //{
            //    StatusMessage = StatusMessage + "Sensitive attributes endpoint suceeded." + Environment.NewLine;
            //}
            //else
            //{
            //    StatusMessage = StatusMessage + "Sensitive attributes endpoint failed." + Environment.NewLine;
            //}

            ////GET Send Attributes

            //var SendAttributesEndPoint = new GETSendAttributes();

            //Sucess = await SendAttributesEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            //if (Sucess)
            //{
            //    StatusMessage = StatusMessage + "Send attributes endpoint suceeded." + Environment.NewLine;
            //}
            //else
            //{
            //    StatusMessage = StatusMessage + "Send attributes endpoint failed." + Environment.NewLine;
            //}

            ////GET Demographic Attributes

            //var DemographicAttributesEndPoint = new GETDemographicAttributes();

            //Sucess = await DemographicAttributesEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            //if (Sucess)
            //{
            //    StatusMessage = StatusMessage + "Demographic attributes endpoint suceeded." + Environment.NewLine;
            //}
            //else
            //{
            //    StatusMessage = StatusMessage + "Demographic attributes endpoint failed." + Environment.NewLine;
            //}


            return true;
        }

    //    public async Task<string> RunTeaching()
    //    {

    //        bool Sucess;

    //        //Add line describing which academy and academic year.
    //        string StatusMessage = pCurrentStatus + pAcademy + " " + pAcYear + " - Teaching Endpoints" + Environment.NewLine;

    //        //GET Departments

    //        GETDepartments DepartmentEndPoint = new GETDepartments();

    //        Sucess = await DepartmentEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

    //        if (Sucess)
    //        {
    //            StatusMessage = StatusMessage + "Department endpoint suceeded." + Environment.NewLine;
    //        }
    //        else
    //        {
    //            StatusMessage = StatusMessage + "Department endpoint failed." + Environment.NewLine;
    //        }

    //        //GET Subjects

    //        GETSubjects SubjectEndPoint = new GETSubjects();

    //        Sucess = await SubjectEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

    //        if (Sucess)
    //        {
    //            StatusMessage = StatusMessage + "Subject endpoint suceeded." + Environment.NewLine;
    //        }
    //        else
    //        {
    //            StatusMessage = StatusMessage + "Subject endpoint failed." + Environment.NewLine;
    //        }

    //        return StatusMessage;
    //    }
    }
}
