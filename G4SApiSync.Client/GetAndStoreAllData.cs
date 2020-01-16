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

            _academyList = _context.AcademySecurity.Where(i => i.Active == true).ToList();
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
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getStudentDetails.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear});
                }
            }

            //GET Education Details
            foreach (var academy in _academyList)
            {
                using (var getEducationDetails = new GETEducationDetails(_context, _connectionString))
                {
                    bool result = await getEducationDetails.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getEducationDetails.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear});
                }
            }

            //GET General Attributes
            foreach (var academy in _academyList)
            {
                using (var getGeneralAttributes = new GETGeneralAttributes(_context, _connectionString))
                {
                    bool result = await getGeneralAttributes.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getGeneralAttributes.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Demographic Attributes
            foreach (var academy in _academyList)
            {
                using (var getDemographicAttributes = new GETDemographicAttributes(_context, _connectionString))
                {
                    bool result = await getDemographicAttributes.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getDemographicAttributes.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Send Attributes
            foreach (var academy in _academyList)
            {
                using (var getSendAttributes = new GETSendAttributes(_context, _connectionString))
                {
                    bool result = await getSendAttributes.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getSendAttributes.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Sensitive Attributes
            foreach (var academy in _academyList)
            {
                using (var getSensitiveAttributes = new GETSensitiveAttributes(_context, _connectionString))
                {
                    bool result = await getSensitiveAttributes.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getSensitiveAttributes.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }


            return syncResults;
        }

//Sync Teaching
        public async Task<List<SyncResult>> SyncTeaching()
        {
            List<SyncResult> syncResults = new List<SyncResult>();

            //GET Departments
            foreach (var academy in _academyList)
            {
                using (var getDepartments = new GETDepartments(_context, _connectionString))
                {
                    bool result = await getDepartments.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getDepartments.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Subjects
            foreach (var academy in _academyList)
            {
                using (var getSubjects = new GETSubjects(_context, _connectionString))
                {
                    bool result = await getSubjects.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getSubjects.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Groups
            foreach (var academy in _academyList)
            {
                using (var getGroups = new GETGroups(_context, _connectionString))
                {
                    bool result = await getGroups.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getGroups.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Group Students
            foreach (var academy in _academyList)
            {
                using (var getGroupStudents = new GETGroupStudents(_context, _connectionString))
                {
                    bool result = await getGroupStudents.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getGroupStudents.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            return syncResults;
        }

//Sync Assessments
        public async Task<List<SyncResult>> SyncAssessment()
        {
            List<SyncResult> syncResults = new List<SyncResult>();

            //GET Markbooks
            foreach (var academy in _academyList)
            {
                using (var getMarkbooks = new GETMarkbooks(_context, _connectionString))
                {
                    bool result = await getMarkbooks.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getMarkbooks.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Marksheet Grades
            foreach (var academy in _academyList)
            {
                using (var getMarksheetGrades = new GETMarksheetGrades(_context, _connectionString))
                {
                    bool result = await getMarksheetGrades.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getMarksheetGrades.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Markslot Marks
            foreach (var academy in _academyList)
            {
                using (var getMarkslotMarks = new GETMarkslotMarks(_context, _connectionString))
                {
                    bool result = await getMarkslotMarks.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getMarkslotMarks.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            return syncResults;
        }

//Sync Attainment
        public async Task<List<SyncResult>> SyncAttainment()
        {
            List<SyncResult> syncResults = new List<SyncResult>();

            //GET Prior Attainment
            foreach (var academy in _academyList)
            {
                using (var getPA = new GETPriorAttainment(_context, _connectionString))
                {
                    bool result = await getPA.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getPA.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Grade Names
            foreach (var academy in _academyList)
            {
                using (var getGradeName = new GETGradeNames(_context, _connectionString))
                {
                    bool result = await getGradeName.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode, academy.LowestYear, academy.HighestYear);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getGradeName.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Grades
            foreach (var academy in _academyList)
            {
                using (var getGrades = new GETGrades(_context, _connectionString))
                {
                    bool result = await getGrades.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode, academy.LowestYear, academy.HighestYear);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getGrades.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            //GET Exam Results
            foreach (var academy in _academyList)
            {
                using (var getExamResults = new GETExamResults(_context, _connectionString))
                {
                    bool result = await getExamResults.UpdateDatabase(academy.APIKey, academy.CurrentAcademicYear, academy.AcademyCode, academy.LowestYear, academy.HighestYear);
                    syncResults.Add(new SyncResult { AcademyCode = academy.AcademyCode, EndPoint = getExamResults.EndPoint, Result = result, LoggedAt = DateTime.Now, DataSet = academy.CurrentAcademicYear });
                }
            }

            return syncResults;
        }
    }
}
