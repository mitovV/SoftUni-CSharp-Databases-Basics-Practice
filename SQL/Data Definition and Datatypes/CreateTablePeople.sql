CREATE TABLE People(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	CHECK(DATALENGTH(Picture) <= 2097152),
	Height DECIMAL(3,2),
	[Weight] DECIMAL(5,2),
	Gender CHAR(1) NOT NULL,
	CHECK(Gender in ('f','m')),
	Birthdate DATETIME NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO People
			([Name], Picture, Height, [Weight], Gender, Birthdate, Biography)
VALUES
			('Pesho', NULL, 1.76, 76, 'm', '12.12.1999',NULL),
			('Gosho', NULL, 1.76, 76, 'm', '12.12.1989',NULL),
			('Misho', NULL, 1.76, 76, 'm', '12.12.1996',NULL),
			('Iva', NULL, 1.76, 76, 'f', '12.12.2000',NULL),
			('Petq', NULL, 1.76, 76, 'f', '12.3.1995',NULL)