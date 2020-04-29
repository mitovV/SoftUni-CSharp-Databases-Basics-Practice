   SELECT f.Id, f.[Name],
		  CONCAT(f.Size, 'KB') AS Size
     FROM Files f
LEFT JOIN Files fl
	   ON fl.ParentId = f.Id
    WHERE fl.ParentId IS NULL
 ORDER BY f.Id, f.[Name], f.Size DESC