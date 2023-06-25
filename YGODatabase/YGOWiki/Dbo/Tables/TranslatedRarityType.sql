CREATE TABLE [dbo].[TranslatedRarityType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalRarityTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedRarityTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedRarityType_OriginalRarityTypeId] FOREIGN KEY ([OriginalRarityTypeId]) REFERENCES [dbo].[RarityType] ([Id]),
	CONSTRAINT [FK_TranslatedRarityType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
