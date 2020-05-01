CREATE TRIGGER tr_DeletedStudents ON [dbo].[Students] FOR DELETE
AS
BEGIN
	INSERT INTO ExcludedStudents
	(
	    [StudentId],
	    [StudentName]
	)
	SELECT d.[Id], d.[FirstName] + ' ' + d.[LastName] FROM [DELETED] AS d
END