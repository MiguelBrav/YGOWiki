﻿CREATE TABLE [dbo].[CardTypes]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_CardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
)