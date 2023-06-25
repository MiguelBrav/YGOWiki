CREATE TABLE [dbo].[TranslatedMonsterType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalMonsterTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedMonsterTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedMonsterType_OriginalMonsterTypeId] FOREIGN KEY ([OriginalMonsterTypeId]) REFERENCES [dbo].[MonsterType] ([Id]),
	CONSTRAINT [FK_TranslatedMonsterType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
