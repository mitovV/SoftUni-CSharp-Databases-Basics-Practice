CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(15, 2))
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @targetId INT = (SELECT Id 
							   FROM Students 
							  WHERE Id = @studentId)

	IF(@targetId IS NULL)
	BEGIN
		RETURN 'The student with provided id does not exist in the school!'
	END
	IF(@grade > 6.00)
	BEGIN
		RETURN 'Grade cannot be above 6.00!'
	END

	DECLARE @count INT = (SELECT COUNT(*)
	                        FROM Students s
							JOIN StudentsExams se
							  ON s.Id = se.StudentId
						   WHERE s.Id = @studentId AND se.Grade BETWEEN @grade AND @grade + 0.50)

	DECLARE @name VARCHAR(50) = (SELECT s.FirstName
	                               FROM Students s
								  WHERE s.Id = @studentId)

	RETURN 'You have to update ' + CAST(@count AS VARCHAR(50)) + ' grades for the student ' + @name
END