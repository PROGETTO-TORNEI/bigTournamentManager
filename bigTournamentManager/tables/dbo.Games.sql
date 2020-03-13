CREATE TABLE [dbo].[games]
(
	[id] INT NOT NULL,
	[name] VARCHAR(100) NOT NULL,
	PRIMARY KEY (id),
	UNIQUE(name)
);