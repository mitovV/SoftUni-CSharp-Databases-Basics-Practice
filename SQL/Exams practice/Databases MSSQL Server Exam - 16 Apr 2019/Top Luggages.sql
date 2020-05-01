  SELECT lt.[Type],
		 COUNT(l.LuggageTypeId) AS MostUsedLuggage
    FROM LuggageTypes lt
    JOIN Luggages l
      ON lt.Id = l.LuggageTypeId
GROUP BY lt.[Type]
ORDER BY MostUsedLuggage DESC, lt.[Type]