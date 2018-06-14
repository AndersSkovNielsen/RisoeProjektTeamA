CREATE TABLE [dbo].[RisoeUdstyr]
(
	[UdstyrId] INT NOT NULL PRIMARY KEY,
	[StationNr] INT,
	[Type] NVARCHAR(40) NOT NULL,
	[InstallationsDato] DATE,
	[Beskrivelse] NVARCHAR(40) NOT NULL,

	FOREIGN KEY (StationNr) REFERENCES RisoeStation (StationNr)
)
