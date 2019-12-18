using G4SApiSync.Client.EndPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4SApiSync.Client
{
    public class GetAndStoreAllData
    {
        private string pAPIKey;
        private string pAcademy;
        private string pAcYear;
        private string pCurrentStatus;

        public GetAndStoreAllData(string APIKey, string Academy, string AcYear, string CurrentStatusMessage)
        {
            pAPIKey = APIKey;
            pAcademy = Academy;
            pAcYear = AcYear;
            pCurrentStatus = CurrentStatusMessage;
        }

        public async Task<string> RunStudents()
        {
            
            bool Sucess;

            //Add line describing which academy and academic year.
            string StatusMessage = pCurrentStatus + pAcademy + " " + pAcYear + " - Student Endpoints" + Environment.NewLine;

            //GET Student Details

            GETStudentDetails StudentEndPoint = new GETStudentDetails();

            Sucess = await StudentEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            if (Sucess)
            {
                //StatusMessage = StatusMessage + "Students endpoint suceeded." + Environment.NewLine;
                Console.WriteLine("Students endpoint suceeded" + Environment.NewLine);
            }
            else
            {
                //StatusMessage = StatusMessage + "Students endpoint failed." + Environment.NewLine;
                Console.WriteLine("Students endpoint failed" + Environment.NewLine);
            }

            //GET Education Details

            var EducationDetailsEndPoint = new GETEducationDetails();

            Sucess = await EducationDetailsEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            if (Sucess)
            {
                StatusMessage = StatusMessage + "Education details endpoint suceeded." + Environment.NewLine;
            }
            else
            {
                StatusMessage = StatusMessage + "Education details endpoint failed." + Environment.NewLine;
            }

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


            return StatusMessage;
        }

        public async Task<string> RunTeaching()
        {

            bool Sucess;

            //Add line describing which academy and academic year.
            string StatusMessage = pCurrentStatus + pAcademy + " " + pAcYear + " - Teaching Endpoints" + Environment.NewLine;

            //GET Departments

            GETDepartments DepartmentEndPoint = new GETDepartments();

            Sucess = await DepartmentEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            if (Sucess)
            {
                StatusMessage = StatusMessage + "Department endpoint suceeded." + Environment.NewLine;
            }
            else
            {
                StatusMessage = StatusMessage + "Department endpoint failed." + Environment.NewLine;
            }

            //GET Subjects

            GETSubjects SubjectEndPoint = new GETSubjects();

            Sucess = await SubjectEndPoint.UpdateDatabase(pAPIKey, pAcYear, pAcademy);

            if (Sucess)
            {
                StatusMessage = StatusMessage + "Subject endpoint suceeded." + Environment.NewLine;
            }
            else
            {
                StatusMessage = StatusMessage + "Subject endpoint failed." + Environment.NewLine;
            }

            return StatusMessage;
        }
    }
}
