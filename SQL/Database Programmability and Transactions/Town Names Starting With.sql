CREATE PROC usp_GetTownsStartingWith(@value VARCHAR(MAX))
AS
BEGIN
	SELECT [Name] 
	  FROM Towns
	 WHERE [Name] LIKE @value + '%'
END