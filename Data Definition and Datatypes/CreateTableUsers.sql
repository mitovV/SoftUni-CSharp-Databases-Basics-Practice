CREATE TABLE Users(
	Id BIGINT PRIMARY KEY IDENTITY,
	Username  VARCHAR(30) UNIQUE NOT NULL, 
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX),
	CHECK(DATALENGTH(ProfilePicture) <= 921600),
	LastLoginTime DATETIME,
	IsDeleted BIT NOT NULL
)

INSERT INTO Users(Username, [Password], ProfilePicture,LastLoginTime, IsDeleted)
VALUES
				 ('Pesho', '123', NULL, NULL, 0),
				 ('Gosho', '123', NULL, NULL,1),
				 ('Ivan', '123', NULL, NULL, 0),
				 ('Gergana', '123', NULL, NULL, 1),
				 ('Kiril', '123', NULL, NULL, 0)

ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC07F96D8B2B

ALTER TABLE Users
ADD CONSTRAINT PK_CompositeIdUsername
PRIMARY KEY (Id, Username) 