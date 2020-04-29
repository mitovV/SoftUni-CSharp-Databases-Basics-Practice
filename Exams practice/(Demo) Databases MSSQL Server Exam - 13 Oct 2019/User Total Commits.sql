CREATE FUNCTION udf_UserTotalCommits(@username VARCHAR(50))
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(*) 
			   FROM Commits 
			  WHERE ContributorId IN (SELECT Id 
										FROM Users 
									   WHERE Username = @username))

END