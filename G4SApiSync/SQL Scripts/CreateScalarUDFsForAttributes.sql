USE G4S;
GO

CREATE OR ALTER FUNCTION g4s.udfAttributeValueAcademicYear(
	@StudentId NVARCHAR(100),
	@Academy NVARCHAR(10),
	@DataSet NVARCHAR(4),
	@AttributeName NVARCHAR(500)
)

RETURNS NVARCHAR(1000)
AS

BEGIN
DECLARE @ReturnValue AS NVARCHAR(1000)
SET @ReturnValue = 
	(SELECT
		v.[Value]
	FROM 
		g4s.AttributeTypes AS t
		LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId
	WHERE 
		t.DataSet = @DataSet
		AND t.Academy = @Academy
		AND t.AttributeName = @AttributeName
		AND v.AcademicYear = @DataSet
		AND v.StudentId = @StudentId)

RETURN @ReturnValue
END;

GO

CREATE OR ALTER FUNCTION g4s.udfAttributeValueNoTimeStamp(
	@StudentId NVARCHAR(100),
	@Academy NVARCHAR(10),
	@DataSet NVARCHAR(4),
	@AttributeName NVARCHAR(500)
)

RETURNS NVARCHAR(1000)
AS

BEGIN
DECLARE @ReturnValue AS NVARCHAR(1000)
SET @ReturnValue = 
	(SELECT
		v.[Value]
	FROM 
		g4s.AttributeTypes AS t
		LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId
	WHERE 
		t.DataSet = @DataSet
		AND t.Academy = @Academy
		AND t.AttributeName = @AttributeName
		AND v.StudentId = @StudentId)

RETURN @ReturnValue
END;
GO

CREATE OR ALTER FUNCTION g4s.udfAttributeValueLatestDate(
	@StudentId NVARCHAR(100),
	@Academy NVARCHAR(10),
	@DataSet NVARCHAR(4),
	@AttributeName NVARCHAR(500)
)

RETURNS NVARCHAR(1000)
AS

BEGIN
DECLARE @ReturnValue AS NVARCHAR(1000)
SET @ReturnValue = 
	(SELECT TOP 1
		v.[Value]
	FROM 
		g4s.AttributeTypes AS t
		LEFT JOIN g4s.AttributeValues AS v ON v.AttributeTypeId = t.AttributeTypeId
	WHERE 
		t.DataSet = @DataSet
		AND t.Academy = @Academy
		AND t.AttributeName = @AttributeName
		AND v.StudentId = @StudentId
	ORDER BY v.[Date] DESC)

RETURN @ReturnValue
END;
GO