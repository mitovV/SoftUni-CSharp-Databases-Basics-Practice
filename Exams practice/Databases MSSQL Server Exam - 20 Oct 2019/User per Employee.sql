   SELECT CONCAT(e.FirstName,' ', e.LastName)  AS FullName,
	      COUNT(u.Username) AS UsersCount
     FROM Employees e
LEFT JOIN Reports r
       ON r.EmployeeId = e.Id
LEFT JOIN Users u
       ON u.Id = r.UserId
 GROUP BY CONCAT(e.FirstName,' ', e.LastName)
 ORDER BY UsersCount DESC, FullName