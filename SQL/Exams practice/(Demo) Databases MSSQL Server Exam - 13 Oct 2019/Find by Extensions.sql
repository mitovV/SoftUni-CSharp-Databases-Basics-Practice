CREATE PROC usp_FindByExtension(@extension VARCHAR(10))
AS
BEGIN
	SELECT f.Id, f.[Name],
		   CONCAT(f.Size,'KB') AS Size
	  FROM Files f
	 WHERE [Name] LIKE '%.' + @extension
	 ORDER BY Id, [Name], f.Size DESC
END