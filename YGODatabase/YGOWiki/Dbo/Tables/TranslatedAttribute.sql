CREATE TABLE [dbo].[TranslatedAttribute]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalAttributeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedAttributeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedAttribute_OriginalAttributeTypeId] FOREIGN KEY ([OriginalAttributeId]) REFERENCES [dbo].[Attribute] ([Id]),
	CONSTRAINT [FK_TranslatedAttribute_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)
