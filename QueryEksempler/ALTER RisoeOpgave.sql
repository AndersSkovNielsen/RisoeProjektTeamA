ALTER TABLE [dbo].[RisoeOpgave]

     ADD [UdstyrId] INT

	 FOREIGN KEY (UdstyrId) REFERENCES RisoeUdstyr (UdstyrId)