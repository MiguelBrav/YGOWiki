CREATE TABLE [dbo].[TranslatedSpecialMonsterType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalSpecialMonsterTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(8000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedSpecialMonsterTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedSpecialMonsterType_OriginalSpecialMonsterTypeId] FOREIGN KEY ([OriginalSpecialMonsterTypeId]) REFERENCES [dbo].[SpecialMonsterType] ([Id]),
	CONSTRAINT [FK_TranslatedSpecialMonsterType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
