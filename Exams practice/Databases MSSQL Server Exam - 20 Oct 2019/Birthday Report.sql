  SELECT u.Username, 
		 c.[Name] AS CategoryName
    FROM Users u
	JOIN Reports r
	  ON r.UserId = u.Id
	JOIN Categories c
	  ON c.Id = r.CategoryId
   WHERE DATEDIFF(DAY, DAY(u.Birthdate), DAY(r.OpenDate)) = 0 AND 
		 DATEDIFF(MONTH,MONTH( u.Birthdate), MONTH(r.OpenDate)) = 0
ORDER BY u.Username, CategoryName