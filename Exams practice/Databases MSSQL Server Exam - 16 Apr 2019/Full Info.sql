  SELECT CONCAT(p.FirstName, ' ', p.LastName) AS [Full Name],
		 pl.[Name] AS [Plane Name],
		 CONCAT(f.Origin, ' - ', f.Destination) AS Trip,
		 lt.[Type] AS [Luggage Type]
    FROM Passengers p
    JOIN Tickets t
      ON t.PassengerId = p.Id
	JOIN Luggages l
	  ON l.Id = t.LuggageId
	JOIN LuggageTypes lt
	  ON lt.Id = l.LuggageTypeId
	JOIN Flights f
	  ON f.Id = t.FlightId
	JOIN Planes pl
	  ON pl.Id = f.PlaneId
ORDER BY [Full Name], [Plane Name], f.Origin, f.Destination, [Luggage Type]
