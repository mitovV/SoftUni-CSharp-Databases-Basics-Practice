  SELECT emp.EmployeeID, emp.FirstName, mng.EmployeeID, mng.FirstName
    FROM Employees emp
	JOIN Employees mng
	  ON emp.ManagerID = mng.EmployeeID
   WHERE mng.EmployeeID IN (3, 7)
ORDER BY emp.EmployeeID