CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(MAX), @word NVARCHAR(MAX)) 
RETURNS BIT
AS
BEGIN
	DECLARE @counter int = 1

	WHILE(@counter <= LEN(@word))
	BEGIN
		DECLARE @churrChar CHAR = SUBSTRING (@word, @counter,1)
		DECLARE @index INT = CHARINDEX(@churrChar, @setOfLetters)

		IF(@index = 0)
		BEGIN
			RETURN 0
		END
		SET	@counter += 1
	END

	RETURN 1
END