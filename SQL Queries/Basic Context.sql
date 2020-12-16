SELECT
    s.Academy,
    s.DataSet,
    s.StudentId,
    e.UPN,
    s.PreferredFirstName AS FirstName,
    s.PreferredLastName AS LastName,
    s.Sex, s.DateOfBirth,
    e.NCYear,
    e.RegistrationGroup AS RegGroup,
    e.AdmissionDate,
    e.LeavingDate,

    MAX(CASE 
        WHEN t.AttributeName = 'On roll' THEN REPLACE(REPLACE(av.value, 'True', 'Yes'), 'False', 'No') 
        ELSE '' END) AS OnRoll,
    MAX(CASE 
        WHEN t.AttributeName = 'Enrollment status' THEN av.value 
        ELSE '' END) AS Enrolled,
    MAX(CASE 
        WHEN t.AttributeName = 'FSM' THEN REPLACE(REPLACE(av.value, 'True', 'Yes'), 'False', 'No') 
        ELSE '' END) AS FSM_Now,
    MAX(CASE 
        WHEN t.AttributeName = 'FSMEver6' THEN REPLACE(REPLACE(av.value, 'True', 'Yes'), 'False', 'No') 
        ELSE '' END) AS FSM6,
    MAX(CASE 
        WHEN t.AttributeName = 'Pupil Premium Indicator' 
        THEN REPLACE(REPLACE(av.value, 'True', 'Yes'), 'False', 'No') ELSE '' END) AS PP,
    MAX(CASE 
        WHEN t.AttributeName = 'Disdavantaged' THEN av.value ELSE '' 
        END) AS Disadvantaged,
    MAX(CASE 
        WHEN t.AttributeName = 'Bursary' THEN REPLACE(REPLACE(av.value, 'True', 'Yes'), 'False', 'No') 
        ELSE '' END) AS Bursary,
    MAX(CASE 
        WHEN t.AttributeName = 'Looked After' THEN REPLACE(REPLACE(av.value, 'True', 'Yes'), 'False', 'No') 
        ELSE '' END) AS LookedAfter,
    MAX(CASE 
        WHEN t.AttributeName = 'Ever in Care' THEN REPLACE(REPLACE(av.value, 'True', 'Yes'), 'False', 'No') 
        ELSE '' END) AS EverInCare,
    MAX(CASE 
        WHEN t.AttributeName = 'EAL' THEN REPLACE(REPLACE(av.value, 'True', 'Yes'),'False', 'No') 
        ELSE '' END) AS EAL,
    MAX(CASE 
        WHEN t.AttributeName = 'Ethnicity' THEN av.value 
        ELSE '' END) AS Ethnicity,
    MAX(CASE 
        WHEN t.AttributeName = 'SEN Code' THEN av.value 
        ELSE '' END) AS SENCode

FROM g4s.Students AS s
    LEFT JOIN g4s.EducationDetails AS e ON e.StudentId = s.StudentId
    LEFT JOIN g4s.AttributeValues AS av ON av.StudentId = s.StudentId
    LEFT JOIN g4s.AttributeTypes AS t ON t.AttributeTypeId = av.AttributeTypeId

GROUP BY 
s.Academy, 
s.DataSet, 
s.StudentId, 
e.UPN, 
s.PreferredFirstName, 
s.PreferredLastName, 
s.Sex, 
s.DateOfBirth, 
e.NCYear, 
e.RegistrationGroup, 
e.AdmissionDate, 
e.LeavingDate