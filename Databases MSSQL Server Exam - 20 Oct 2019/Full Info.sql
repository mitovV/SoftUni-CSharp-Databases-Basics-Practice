   SELECT IIF(e.FirstName IS NULL OR e.LastName IS NULL, 'None',  CONCAT(e.FirstName, ' ', e.LastName)) AS [Employee],
          IIF(d.[Name] IS NULL, 'None', d.[Name]) AS [Department],
	      c.[Name] AS [Category],
	      r.[Description],
		  FORMAT(r.OpenDate, 'dd.MM.yyyy') AS [OpenDate],
		  s.[Label] AS [Status],
		  u.[Name] AS [User]
	 FROM Reports r
LEFT JOIN Employees e 
	   ON r.EmployeeId = e.Id
LEFT JOIN Departments d 
	   ON e.DepartmentId = d.Id
LEFT JOIN Categories c 
	   ON r.CategoryId = c.Id
LEFT JOIN Users u 
	   ON r.UserId = u.Id
LEFT JOIN STATUS s 
	   ON s.Id = r.StatusId
 ORDER BY e.FirstName DESC, e.LastName, [Department], [Category], r.[Description], r.OpenDate, s.Id, u.Id