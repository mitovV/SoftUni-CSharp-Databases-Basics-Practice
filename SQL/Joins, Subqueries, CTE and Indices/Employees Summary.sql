  SELECT TOP(50)emp.EmployeeID, 
         CONCAT(emp.FirstName, ' ', emp.LastName) AS EmployeeName,
		 CONCAT(mng.FirstName, ' ', mng.LastName) AS ManagerName,
		 d.[Name]
    FROM Employees emp
	JOIN Employees mng
	  ON emp.ManagerID = mng.EmployeeID
	JOIN Departments d
	  ON d.DepartmentID = emp.DepartmentID
ORDER BY emp.EmployeeID