CREATE TABLE [dbo].[players]
(
	[id] INT NOT NULL,
	[nickname] VARCHAR(100) NOT NULL,
	[name] VARCHAR(200) NOT NULL,
	[lastname] VARCHAR(200) NOT NULL,
	[elo] FLOAT NOT NULL,
	[email] VARCHAR(200) NOT NULL,
	[phone] VARCHAR(15) NOT NULL,
	[birthdate] DATE NOT NULL,	
	PRIMARY KEY (id),
	UNIQUE(nickname)
);