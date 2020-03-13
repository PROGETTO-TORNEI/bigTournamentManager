CREATE TABLE [dbo].[tables_players]
(
	[id] INT NOT NULL,
	[ky_player] INT NOT NULL,
	[ky_table] INT NOT NULL,
	[score] DECIMAL(10, 2) NOT NULL,
	PRIMARY KEY (id),
	CONSTRAINT FK_compounds_tables FOREIGN KEY (ky_table) REFERENCES tables(id),
	CONSTRAINT FK_compounds_players FOREIGN KEY (ky_player) REFERENCES players(id)
);