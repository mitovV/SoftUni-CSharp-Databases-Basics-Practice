  SELECT TOP(5) r.Id, r.[Name], 
         COUNT(c.RepositoryId) AS Commits
    FROM Repositories r
    JOIN Commits c
      ON c.RepositoryId = r.Id
    JOIN RepositoriesContributors rc
	  ON rc.RepositoryId = r.Id
GROUP BY r.Id,r.[Name]
ORDER BY Commits DESC, r.Id, r.[Name]
