--This returns marksheet grades for any sheet ending in "Forecast"
--G4S records Reception year group in primary as "Reception" hence dealing with years as strings.

SELECT
    s.studentid,
    s.Academy,
    s.dataset,
    d.UPN,
    d.NCYear,
    s.PreferredFirstName,
    s.PreferredLastName,
    d.RegistrationGroup as [RegGroup],
    sb.[Name] AS [SubjectName],
    'Forecast' AS [GradeName],


    CASE mg.Alias
        WHEN NULL THEN mg.Grade
        ELSE mg.Grade 
    END AS [Grade],
    
    CONVERT(nvarchar, d.NCYear) AS [SlotYear],
    
    NULL AS [SlotTerm]


FROM g4s.Students AS s
    LEFT JOIN g4s.EducationDetails AS d ON s.StudentId = d.StudentId
    LEFT JOIN g4s.MarksheetGrades AS mg ON s.StudentId = mg.StudentId
    LEFT JOIN g4s.Marksheets AS ms ON mg.MarksheetId = ms.MarksheetId
    LEFT JOIN g4s.Subjects AS sb ON ms.SubjectId = sb.SubjectId


WHERE  
d.NCYear IN ('1', '2', '3', '4', '5', '6')
    AND s.DataSet = 2021
    AND sb.[Name] IS NOT NULL
    AND ms.[Name] LIKE '%Forecast'