CREATE TABLE [dbo].[TranslatedMonsterCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalMonsterCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(8000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedMonsterCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedMonsterCardType_OriginalMonsterCardTypeId] FOREIGN KEY ([OriginalMonsterCardTypeId]) REFERENCES [dbo].[MonsterCardType] ([Id]),
	CONSTRAINT [FK_TranslatedMonsterCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
