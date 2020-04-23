CREATE PROC usp_GetEmployeesFromTown(@townName NVARCHAR(MAX))
AS
BEGIN
	SELECT FirstName, LastName
	  FROM Employees e
	  JOIN Addresses a
		ON a.AddressID = e.AddressID
	  JOIN Towns t
	    ON t.TownID = a.TownID
	 WHERE t.[Name] = @townName
END