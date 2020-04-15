  SELECT DepartmentID, RankingTable.Salary AS ThirdHighestSalary 
    FROM (SELECT DepartmentID, Salary, DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS Ranking
    FROM Employees
GROUP BY Salary,DepartmentID) AS RankingTable
   WHERE RankingTable.Ranking = 3
ORDER BY DepartmentID