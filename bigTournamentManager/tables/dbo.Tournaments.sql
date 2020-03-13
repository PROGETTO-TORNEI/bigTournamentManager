CREATE TABLE [dbo].[tournaments]
(
	[id] INT NOT NULL,
	[date_hour] DATETIME NOT NULL,
	[name] VARCHAR(100) NOT NULL,
	[players_n] INT NOT NULL,
	[description] TEXT NOT NULL,
	[online] SMALLINT NOT NULL,
	[table_progressive] INT NOT NULL,
	[final_phase] BIT NOT NULL,
	[address] VARCHAR(250) NOT NULL,
	PRIMARY KEY (id)
);