CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN
	DECLARE @EmployeeDepartmentName VARCHAR(MAX) = (SELECT d.[Name] 
													  FROM Employees e 
													  JOIN Departments d
														ON d.Id = e.DepartmentId
													 WHERE e.Id = @EmployeeId)

	DECLARE @ReportDepartmentName VARCHAR(MAX) = (SELECT d.[Name] 
													FROM Reports r
													JOIN Categories c
													  ON c.Id = r.CategoryId
													JOIN Departments d
													  ON d.Id = c.DepartmentId
												   WHERE r.Id = @ReportId)

	IF(@EmployeeDepartmentName != @ReportDepartmentName)
	BEGIN
		RAISERROR('Employee doesn''t belong to the appropriate department!', 16,1)
	END

	UPDATE Reports
	   SET EmployeeId = @EmployeeId
	 WHERE Id = @ReportId
END