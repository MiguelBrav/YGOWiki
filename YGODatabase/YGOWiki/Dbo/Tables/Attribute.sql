﻿CREATE TABLE [dbo].[Attribute]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_AttributeId] PRIMARY KEY CLUSTERED ([Id] ASC)
)
