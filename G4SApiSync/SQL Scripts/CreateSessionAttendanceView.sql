USE G4S;
GO

CREATE OR ALTER VIEW g4s.v_SessionAttendance
AS

SELECT
	ed.DataSet,
	ed.Academy,
	s.StudentId,
	ed.UPN,
	sm.[Date],
	sm.[Session],
	ac.Code AS [Attendance Code],
	ac.AttendanceLabel AS [Attendance Code Label],
	aac.AliasCode AS [Attendance Alias Code],
	aac.[Label] AS [Attendance Alias Code Label],
	sm.SessionMinutesLate AS [Session Minutes Late],
	sm.SessionNotes AS [Session Notes]

FROM g4s.StudentSessionMarks AS sm
LEFT JOIN g4s.Students AS s ON s.StudentId = sm.StudentId
LEFT JOIN g4s.EducationDetails AS ed ON s.StudentId = ed.StudentId
LEFT JOIN g4s.AttendanceCodes AS ac ON sm.SessionMarkId = ac.AttendanceCodeId
LEFT JOIN g4s.AttendanceAliasCodes AS aac ON sm.SessionAliasId = aac.AttendanceAliasCodeId