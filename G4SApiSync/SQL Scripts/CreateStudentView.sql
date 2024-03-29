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
		
		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Unique Learner Number')  AS [ULN],

		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'On roll') 
			WHEN 'True' THEN CAST(1 AS bit)
			ELSE CAST(0 AS bit)
		END AS [OnRoll],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'FSM') 
			WHEN 'True' THEN 'FSM'
			ELSE 'Not FSM'
		END AS NVARCHAR(50) ) AS [FSM],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'FSMEver6') 
			WHEN 'True' THEN 'FSM Ever6'
			ELSE 'Not FSM Ever6'
		END AS NVARCHAR(50) ) AS [FSMEver6],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Pupil Premium Indicator') 
			WHEN 'True' THEN 'PP'
			ELSE 'Not PP'
		END AS NVARCHAR(50) )AS [PP],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Disadvantaged')
			WHEN 'True' THEN 'Disadvantaged'
			ELSE 'Not Disadvantaged'
		END AS NVARCHAR(50) )AS [Disadvantaged],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Bursary') 
			WHEN 'True' THEN 'Bursary'
			ELSE 'No Bursary'
		END AS NVARCHAR(50) )AS [Bursary],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Looked After') 
			WHEN 'True' THEN 'CLA'
			ELSE 'Not CLA'
		END AS NVARCHAR(50) )AS [ChildLookedAfter],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Ever in Care')
			WHEN 'True' THEN 'Ever In Care' 
			ELSE 'Not Ever in Care' 
		END AS NVARCHAR(50) )AS [EverInCare],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'EAL') 
			WHEN 'True' THEN 'EAL'
			ELSE 'Not EAL' 
		END AS NVARCHAR(50) )AS [EAL],

		g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'Ethnicity Code') AS [Ethnicity],

		CAST(
		CASE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'SEN Code') 
			WHEN NULL THEN 'N'
			ELSE g4s.udfAttributeValueLatest(s.StudentId, s.Academy, s.DataSet, 'SEN Code') 
		END AS NVARCHAR(50) ) AS [SENCode],
		p.Value AS [KS2 Band]

	FROM 
		g4s.Students AS s
		LEFT JOIN g4s.EducationDetails AS e ON e.StudentId = s.StudentId
		LEFT JOIN g4s.PriorAttainment AS p ON s.StudentId = p.StudentId AND p.Name = 'Prior Attainment (KS2)';
GO