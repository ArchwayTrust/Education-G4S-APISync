--Example code for pulling data from marksheet columns.

SELECT
    s.studentid,
    s.Academy,
    s.Dataset,
    d.UPN,
    CONVERT(nvarchar, d.NCYear) AS [NCYear],
    s.PreferredFirstName,
    s.PreferredLastName,
    d.RegistrationGroup as [RegGroup],
    ms.[Name] AS [MarksheetName],
    msl.[Name] AS [MarkslotName],
    msm.Grade AS [MarkslotGrade],
    msm.Mark AS [MarkslotMark],
    msm.Alias AS [MarkslotALias]


FROM g4s.Students AS s
    LEFT JOIN g4s.EducationDetails AS d ON s.StudentId = d.StudentId
    LEFT JOIN g4s.MarkslotMarks AS msm ON s.StudentId = msm.StudentId
    LEFT JOIN g4s.MarkSlots AS msl ON msm.MarkslotId = msl.MarkslotId
    LEFT JOIN g4s.Marksheets AS ms ON ms.MarksheetId = msl.MarksheetId
    LEFT JOIN g4s.Subjects AS sb ON ms.SubjectId = sb.SubjectId


WHERE  
d.NCYear IN ('1', '2', '3', '4', '5', '6')
    AND s.DataSet = 2021
    AND sb.[Name] IS NOT NULL
    AND ms.[Name] IN ('NFER', 'NFER GP') --Put the marksheet names you want here.