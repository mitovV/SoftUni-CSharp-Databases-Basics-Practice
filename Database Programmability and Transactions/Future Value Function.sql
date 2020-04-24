CREATE FUNCTION ufn_CalculateFutureValue(@sum MONEY, @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS MONEY
BEGIN
	RETURN @sum * POWER(1 + @yearlyInterestRate, @numberOfYears)
END