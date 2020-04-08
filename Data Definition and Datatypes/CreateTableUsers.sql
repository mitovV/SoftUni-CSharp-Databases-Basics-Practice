CREATE TABLE Users(
	Id BIGINT PRIMARY KEY IDENTITY,
	Username  VARCHAR(30) UNIQUE NOT NULL, 
	[Password] VARCHAR(26) NOT NULL,
	CHECK(LEN([Password]) >= 5),
	ProfilePicture VARBINARY(MAX),
	CHECK(DATALENGTH(ProfilePicture) <= 921600),
	LastLoginTime DATETIME2,
	IsDeleted BIT NOT NULL
)

INSERT INTO Users(Username, [Password], ProfilePicture,LastLoginTime, IsDeleted)
VALUES
				 ('Pesho', '12345', NULL, NULL, 0),
				 ('Gosho', '123456', NULL, NULL,1),
				 ('Ivan', '1234567', NULL, NULL, 0),
				 ('Gergana', '123456', NULL, NULL, 1),
				 ('Kiril', '123456', NULL, NULL, 0)

ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC079142EC28

ALTER TABLE Users
ADD CONSTRAINT PK_CompositeIdUsername
PRIMARY KEY (Id, Username) 

ALTER TABLE Users
ADD CONSTRAINT DF_LoginTime
DEFAULT GETDATE() FOR LastLoginTime

INSERT INTO Users (Username, [Password], ProfilePicture, IsDeleted)
VALUES 
				  ('KiriLl', '123456', NULL, 0)

SELECT * FROM Users