CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(7)
AS
BEGIN
	DECLARE @salaryLevel NVARCHAR(7)
	IF(@salary < 30000)
	BEGIN
		SET @salaryLevel = 'Low'
	END

	ELSE IF (@salary <= 50000)
	BEGIN
		SET @salaryLevel = 'Average'
	END
	
	ELSE
	BEGIN
		SET @salaryLevel = 'High'
	END

	RETURN @salaryLevel
END