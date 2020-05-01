  SELECT CONCAT(p.FirstName, ' ', p.LastName) AS [Full Name],
		 f.Origin,
		 f.Destination
    FROM Passengers p
    JOIN Tickets t
      ON p.Id = t.PassengerId
	JOIN Flights f
	  ON f.Id = t.FlightId
ORDER BY [Full Name], Origin, Destination
