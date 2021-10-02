CREATE OR ALTER VIEW g4s.v_Students
AS
WITH 
--Get enrollment Status
cte_EnrollmentStatus
AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    v.Value

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'Enrollment status'),

--Get On Roll Flag
cte_OnRoll AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'On Roll'
		ELSE 'Left Academy'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'On roll' ),

--Get FSM
cte_FSM AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'FSM'
		ELSE 'Not FSM'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'FSM' ),

    
--Get FSM Ever6
cte_FSMEver6 AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'FSM Ever 6'
		ELSE 'Not FSM Ever 6'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'FSMEver6' ),

--Get PP Indicator
cte_PP AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'Pupil Premium'
		ELSE 'Not Pupil Premium'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'Pupil Premium Indicator' ),

--Get Disadvantaged
cte_DA AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'Disadvantaged'
		ELSE 'Not Disadvantaged'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'Disadvantaged' ),


--Get Bursary
cte_Bursary AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'Recieves Bursary'
		ELSE 'No Bursary'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'Bursary' ),

--Get Looked After
cte_LookedAfter AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'Child Looked After'
		ELSE 'Not a Child Looked After'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'Looked After' ),


--Get Ever In Care
--This has no date or year stamp.
cte_EverInCare AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'Has Been In Care'
		ELSE 'Has Not Been In Care'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'Ever in Care' ),

--Get EAL
--This has no date or year stamp.
cte_EAL AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN 'True' THEN 'EAL'
		ELSE 'Not EAL'
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'EAL' ),


--Get Ethnicity
--This has no date or year stamp.
cte_Ethnicity AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN NULL THEN 'Not Available'
		ELSE v.Value
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId

WHERE t.AttributeName = 'Ethnicity' ),

--Get SEN Status
cte_SEN AS (
SELECT 
    v.StudentId,
    v.AcademicYear,
    v.Date,
    CASE v.Value
		WHEN NULL THEN 'N'
		ELSE v.Value
	END AS [Value]

FROM g4s.AttributeTypes AS t
LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId 

WHERE t.AttributeName = 'SEN Code' )

--Combine it all

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
    es.Value AS [EnrollmentStatus],
    r.Value AS [OnRoll],
    fm.Value AS [FSM],
    fme.Value AS [FSMEver6],
    fpp.Value AS [PP],
    da.Value AS [Disadvantaged],
    bur.Value AS [Bursary],
    la.Value AS [ChildLookedAfter],
    ec.Value AS [EverInCare],
    eal.Value AS [EAL],
    eth.Value AS [Ethnicity],
    sen.Value AS [SENCode],
    p.Value AS [KS2PA]

FROM g4s.Students AS s
    LEFT JOIN g4s.EducationDetails AS e ON e.StudentId = s.StudentId
    LEFT JOIN g4s.PriorAttainment AS p ON s.StudentId = p.StudentId AND p.Name = 'Prior Attainment (KS2)'
    LEFT JOIN cte_EnrollmentStatus AS es ON s.StudentId = es.StudentId
    LEFT JOIN cte_OnRoll AS r ON s.StudentId = r.StudentId
    LEFT JOIN cte_FSM AS fm ON s.StudentId = fm.StudentId
    LEFT JOIN cte_FSMEver6 AS fme ON s.StudentId = fme.StudentId
    LEFT JOIN cte_PP AS fpp ON s.StudentId = fpp.StudentId
    LEFT JOIN cte_DA AS da ON s.StudentId = da.StudentId
    LEFT JOIN cte_Bursary AS bur ON s.StudentId = bur.StudentId
    LEFT JOIN cte_LookedAfter la ON s.StudentId = la.StudentId
    LEFT JOIN cte_EverInCare ec ON s.StudentId = ec.StudentId
    LEFT JOIN cte_EAL eal ON s.StudentId = eal.StudentId
    LEFT JOIN cte_Ethnicity eth ON s.StudentId = eth.StudentId
    LEFT JOIN cte_SEN sen ON s.StudentId = sen.StudentId