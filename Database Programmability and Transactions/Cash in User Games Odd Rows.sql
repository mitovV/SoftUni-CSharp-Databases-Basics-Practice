CREATE FUNCTION ufn_CashInUsersGames(@gameName VARCHAR(MAX))
RETURNS @output TABLE(SumCash DECIMAL(18,4))
BEGIN
INSERT INTO @output	SELECT SUM(Cash) 
	  FROM (SELECT *, ROW_NUMBER() OVER(ORDER BY Cash DESC) AS [Row]  
					   FROM UsersGames
					  WHERE GameId IN (SELECT Id 
										 FROM Games
										WHERE [Name] = @gameName)) AS q
	 WHERE q.[Row] % 2 <> 0	 
RETURN
END