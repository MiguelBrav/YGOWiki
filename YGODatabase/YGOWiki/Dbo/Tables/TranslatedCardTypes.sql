CREATE TABLE [dbo].[TranslatedCardTypes]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(8000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedCardType_OriginalCardTypeId] FOREIGN KEY ([OriginalCardTypeId]) REFERENCES [dbo].[CardTypes] ([Id]),
	CONSTRAINT [FK_TranslatedCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
