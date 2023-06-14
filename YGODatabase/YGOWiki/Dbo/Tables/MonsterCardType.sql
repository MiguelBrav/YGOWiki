CREATE TABLE [dbo].[MonsterCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalCardTypeId] INT NOT NULL,
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_MonsterCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_MonsterCardType_OriginalCardTypeId] FOREIGN KEY ([OriginalCardTypeId]) REFERENCES [dbo].[CardTypes] ([Id]),

)
