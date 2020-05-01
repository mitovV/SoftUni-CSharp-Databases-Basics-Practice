CREATE FUNCTION udf_CalculateTickets(@origin VARCHAR(50), @destination VARCHAR(50), @peopleCount INT)
RETURNS VARCHAR(50)
AS
BEGIN
	IF(@peopleCount <= 0)
	BEGIN
		RETURN 'Invalid people count!'
	END

	DECLARE @flightId INT = (SELECT Id 
						      FROM Flights
						     WHERE Origin = @origin AND Destination = @destination)

	IF(@flightId IS NULL)
	BEGIN 
		RETURN 'Invalid flight!'
	END

	DECLARE @price DECIMAL(18,2) = @peopleCount *  (SELECT Price FROM Tickets WHERE FlightId = @flightId)


	RETURN CONCAT('Total price ', @price)
END