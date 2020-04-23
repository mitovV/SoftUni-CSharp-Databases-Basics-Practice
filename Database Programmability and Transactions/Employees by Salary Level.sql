CREATE PROC usp_EmployeesBySalaryLevel(@level VARCHAR(7))
AS
BEGIN
	SELECT q.FirstName, q.LastName 
	  FROM (SELECT e.FirstName, e.LastName, dbo.ufn_GetSalaryLevel(e.Salary) AS [Salary Level]
	          FROM Employees e) AS q
	 WHERE q.[Salary Level] = @level
END