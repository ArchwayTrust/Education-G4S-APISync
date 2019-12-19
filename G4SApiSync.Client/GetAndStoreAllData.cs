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
        private readonly List<AcademySecurity> _academyList;
        private readonly G4SContext _context;
        private readonly string _connectionString;

        public GetAndStoreAllData(G4SContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;

            _academyList = _context.AcademySecurity.ToList();
        }

        public async Task<List<SyncResult>> SyncStudents()
        {
            List<SyncResult> syncResults = new List<SyncResult>();

            //GET Student Details
            foreach(var academy in _academyList)
            {
                using (var getStudentDetails = new GETStudentDetails(_context, _connectionString))
                {
                    bool result = await getStudentDetails.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getStudentDetails.EndPoint, Result = result, LoggedAt = DateTime.Now });
                }
            }

            //GET Education Details
            foreach (var academy in _academyList)
            {
                using (var getEducationDetails = new GETEducationDetails(_context, _connectionString))
                {
                    bool result = await getEducationDetails.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getEducationDetails.EndPoint, Result = result, LoggedAt = DateTime.Now });
                }
            }


            return syncResults;


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


            //return syncResults;
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
