  SELECT q.FirstName, q.LastName, q.Destination, q.Price
	FROM (SELECT p.FirstName, p.LastName, f.Destination, t.Price, 
				 DENSE_RANK() OVER(PARTITION BY p.FirstName, p.LastName ORDER BY t.Price DESC) AS RankPrice
			FROM Passengers p
			JOIN Tickets t
			  ON t.PassengerId = p.Id
			JOIN Flights f
			  ON f.Id = t.FlightId)	AS q
   WHERE q.RankPrice = 1
ORDER BY q.Price DESC, q.FirstName, q.LastName, q.Destination