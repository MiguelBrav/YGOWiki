CREATE TABLE [dbo].[TranslatedBanlistType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalBanlistTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedBanlistTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedBanlistType_OriginalBanlistTypeId] FOREIGN KEY ([OriginalBanlistTypeId]) REFERENCES [dbo].[BanlistType] ([Id]),
	CONSTRAINT [FK_TranslatedBanlistType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
