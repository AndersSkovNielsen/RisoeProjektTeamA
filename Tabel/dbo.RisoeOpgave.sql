CREATE TABLE [dbo].[RisoeOpgave]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Beskrivelse] VARCHAR(200) NOT NULL, 
    [Status] NVARCHAR(10) NOT NULL, 
    [VentetidIDage] INT NOT NULL
)
