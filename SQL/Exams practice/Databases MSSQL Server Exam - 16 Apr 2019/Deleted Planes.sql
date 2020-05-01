CREATE TRIGGER tr_DeletePlain ON Planes FOR DELETE
AS
BEGIN
	INSERT INTO DeletedPlanes(Id, [Name], Seats, [Range]) SELECT * FROM deleted
END