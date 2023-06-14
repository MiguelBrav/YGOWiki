CREATE TABLE [dbo].[TranslatedTrapCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalTrapCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(8000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedTrapCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedTrapCardType_OriginalTrapCardTypeId] FOREIGN KEY ([OriginalTrapCardTypeId]) REFERENCES [dbo].[TrapCardType] ([Id]),
	CONSTRAINT [FK_TranslatedTrapCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
