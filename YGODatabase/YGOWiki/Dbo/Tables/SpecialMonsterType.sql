CREATE TABLE [dbo].[SpecialMonsterType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_SpecialMonsterTypeId] PRIMARY KEY CLUSTERED ([Id] ASC)
)
