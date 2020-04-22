SELECT MIN(k.Salary) AS MinAverageSalary 
  FROM ( SELECT AVG(Salary) AS Salary
		   FROM Employees
	   GROUP BY DepartmentID) AS k