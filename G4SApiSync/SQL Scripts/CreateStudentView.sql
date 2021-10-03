USE G4S;
GO

CREATE OR ALTER VIEW g4s.v_Students
AS

	SELECT
		s.Academy,
		s.DataSet,
		s.StudentId,
		e.UPN,
		s.PreferredFirstName AS FirstName,
		s.PreferredLastName AS LastName,
		s.Sex AS Gender, 
		s.DateOfBirth,

		CASE 
			WHEN e.RegistrationGroup = 'Ducklings' THEN -1
			WHEN e.NCYear = 'Reception' THEN 0
			ELSE CONVERT(int, e.NCYear) 
		END AS NCYear,

		e.RegistrationGroup AS RegGroup,
		e.AdmissionDate,
		e.LeavingDate,
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Enrollment status') AS [EnrollmentStatus],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'On roll') AS [OnRoll],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'FSM') AS [FSM],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'FSMEver6') AS [FSMEver6],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Pupil Premium Indicator') AS [PP],
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Disadvantaged')
			WHEN 'True' THEN 'True'
			ELSE 'False'
		END AS [Disadvantaged],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Bursary') AS [Bursary],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Looked After') AS [ChildLookedAfter],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Ever in Care') AS [EverInCare],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'EAL') AS [EAL],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Ethnicity')AS [Ethnicity],
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'SEN Code') AS [SENCode],
		p.Value AS [KS2PA]

	FROM 
		g4s.Students AS s
		LEFT JOIN g4s.EducationDetails AS e ON e.StudentId = s.StudentId
		LEFT JOIN g4s.PriorAttainment AS p ON s.StudentId = p.StudentId AND p.Name = 'Prior Attainment (KS2)';
GO