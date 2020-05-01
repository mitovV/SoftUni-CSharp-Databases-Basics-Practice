  SELECT e.EmployeeID, e.FirstName, IIF(p.StartDate >= '01.01.2005',NULL , p.[Name])
    FROM Employees e
	JOIN EmployeesProjects ep
	  ON ep.EmployeeID = e.EmployeeID
	JOIN Projects p
	  ON p.ProjectID = ep.ProjectID
   WHERE e.EmployeeID = 24