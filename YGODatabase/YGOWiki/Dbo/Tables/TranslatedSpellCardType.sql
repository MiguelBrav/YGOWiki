CREATE TABLE [dbo].[TranslatedSpellCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalSpellCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(8000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedSpellCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedSpellCardType_OriginalSpellCardTypeId] FOREIGN KEY ([OriginalSpellCardTypeId]) REFERENCES [dbo].[SpellCardType] ([Id]),
	CONSTRAINT [FK_TranslatedSpellCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
