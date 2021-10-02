SELECT DISTINCT
    s.Academy,
    d.UPN,
    d.NCYear,
    s.PreferredFirstName,
    s.PreferredLastName,
    d.RegistrationGroup as [RegGroup],
    sb.Name AS [SubjectName],
    sb.QualificationTitle,
    n.[Name] AS [GradeName],


    CASE g.Alias
WHEN NULL THEN g.[Name]
ELSE g.[Name] END AS [Grade]


FROM g4s.Students AS s
    LEFT JOIN g4s.EducationDetails AS d ON s.StudentId = d.StudentId
    LEFT JOIN g4s.Grades AS g ON g.StudentId = s.StudentId
    LEFT JOIN g4s.GradeTypes AS t ON t.GradeTypeId = g.GradeTypeId
    LEFT JOIN g4s.GradeNames AS n ON n.GradeTypeId = t.GradeTypeId AND n.DataSet = g.DataSet AND n.NCYear = g.NCYear
    LEFT JOIN g4s.Subjects AS sb ON sb.SubjectId = g.SubjectId


WHERE 
d.NCYear IN ('10', '11')
    AND s.DataSet = 2021
    AND sb.[Name] IS NOT NULL