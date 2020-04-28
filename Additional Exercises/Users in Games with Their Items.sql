  SELECT u.Username, g.[Name], 
		 COUNT(ugi.ItemId) AS [Items Count], 
		 SUM(i.Price) AS [Items Price]
    FROM Users u
    JOIN UsersGames ug
	  ON ug.UserId = u.Id
	JOIN Games g
	  ON g.Id = ug.GameId
	JOIN UserGameItems ugi
	  ON ugi.UserGameId = ug.Id
	JOIN Items i
	  ON i.Id = ugi.ItemId
GROUP BY u.Username, g.[Name]
  HAVING COUNT(ugi.ItemId) >= 10
ORDER BY [Items Count] DESC, [Items Price] DESC, u.Username