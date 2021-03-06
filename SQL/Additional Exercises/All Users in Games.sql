  SELECT g.[Name] AS Game, 
		 gt.[Name] AS [Game Type],
		 u.Username,ug.[Level], 
		 ug.Cash, 
		 c.[Name] AS [Character]
    FROM Users u
    JOIN UsersGames ug
      ON ug.UserId = u.Id
    JOIN Games g
      ON g.Id = ug.GameId
    JOIN GameTypes gt
      ON gt.Id = g.GameTypeId
    JOIN Characters c
      ON c.Id = ug.CharacterId
ORDER BY [Level] DESC, u.Username, Game