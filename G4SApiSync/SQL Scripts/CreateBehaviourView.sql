USE G4S;
GO

CREATE OR ALTER VIEW g4s.v_Behaviour
AS

SELECT 
	e.BehEventId,
	e.Academy,
	e.DataSet,
	st.StudentId,
	ed.UPN,
	st.PreferredFirstName AS [Student First Name],
	st.PreferredLastName AS [Student Last Name],
	bc.Name AS [Behaviour Class],
	bc.Score,
	et.Code AS [Event Code],
	et.Name AS [Event Type],
	e.SubjectCode,
	sb.[Name] AS [Subject],
	e.YearGroup,
	e.GroupName,
	et.Significance,
	e.EventDate AS [Event Date],
	s.FirstName AS [Created by First Name],
	s.LastName AS [Created by Last Name],
	s.EmailAddress AS [Created By Email],
	e.SchoolNotes,
	e.Cancelled,
	e.Closed


FROM g4s.BehEvents AS e
LEFT JOIN g4s.BehEventTypes AS et ON et.BehEventTypeId = e.BehEventTypeId
LEFT JOIN g4s.BehClassifications AS bc ON bc.BehClassificationId = et.BehClassificationId
LEFT JOIN g4s.Staff AS s ON s.StaffId = e.CreatedByStaffId
LEFT JOIN g4s.BehEventStudents AS bs ON bs.BehEventId = e.BehEventId
LEFT JOIN g4s.Students AS st ON bs.StudentId = st.StudentId
LEFT JOIN g4s.EducationDetails AS ed ON ed.StudentId = st.StudentId
LEFT JOIN g4s.Subjects AS sb ON sb.Academy = e.Academy AND sb.Code = e.SubjectCode AND sb.YearGroup = e.YearGroup AND sb.DataSet = e.DataSet;
