  SELECT TOP 5 e.FirstName, e.LastName, p.[Name]
    FROM Employees e
    JOIN EmployeesProjects ep 
      ON ep.EmployeeID = e.EmployeeID
    JOIN Projects p
      ON p.ProjectID = ep.ProjectID
   WHERE p.StartDate > '08.13.2002' AND p.EndDate IS NULL
ORDER BY e.EmployeeID
