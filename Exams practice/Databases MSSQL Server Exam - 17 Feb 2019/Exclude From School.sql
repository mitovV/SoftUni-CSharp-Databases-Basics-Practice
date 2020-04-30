CREATE PROCEDURE usp_ExcludeFromSchool(@StudentId INT)
AS
BEGIN
	DECLARE @targetId INT = (SELECT Id FROM Students WHERE Id = @StudentId)

	IF(@targetId IS NULL)
	BEGIN
		RAISERROR('This school has no student with the provided id!', 16, 1)
		RETURN
	END

	DELETE
	  FROM StudentsTeachers
	 WHERE StudentId = @StudentId

	DELETE
	  FROM StudentsSubjects
	 WHERE StudentId = @StudentId

	DELETE
	  FROM StudentsExams
	 WHERE StudentId = @StudentId

	DELETE
	  FROM Students
	 WHERE Id = @StudentId
END