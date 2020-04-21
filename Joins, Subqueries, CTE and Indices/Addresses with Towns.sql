  SELECT TOP (50) e.FirstName, e.LastName, t.[Name], a.AddressText
    FROM Employees e
    JOIN Addresses a
      ON a.AddressID = e.AddressID
	JOIN Towns t
	  ON T.TownID = a.TownID
ORDER BY e.FirstName, e.LastName