﻿CREATE TABLE [dbo].[Address]
(
	[Id] BIGINT IDENTITY (1,1)  NOT NULL PRIMARY KEY,
	[Address] VARCHAR(255) NOT NULL,
	[ZipCode] VARCHAR(20) NOT NULL,
	[City] VARCHAR(50) NOT NULL,
)
