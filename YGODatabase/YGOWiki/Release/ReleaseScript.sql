CREATE TABLE [dbo].[Attribute]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_AttributeId] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[BanlistType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_BanlistTypeId] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[CardTypes]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_CardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
)

CREATE TABLE [dbo].[Language]
(
	[Id] CHAR(5) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_LanguageId] PRIMARY KEY CLUSTERED ([Id] ASC),
)

CREATE TABLE [dbo].[MonsterType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_MonsterTypeId] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[RarityType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[Description] NVARCHAR(100) NOT NULL,
	[IsSpecial]	BIT DEFAULT 0,
	CONSTRAINT [PK_RarityTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
)

CREATE TABLE [dbo].[SpecialMonsterType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_SpecialMonsterTypeId] PRIMARY KEY CLUSTERED ([Id] ASC)
)

----------------------------------------------------------------------------------

CREATE TABLE [dbo].[MonsterCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalCardTypeId] INT NOT NULL,
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_MonsterCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_MonsterCardType_OriginalCardTypeId] FOREIGN KEY ([OriginalCardTypeId]) REFERENCES [dbo].[CardTypes] ([Id]),

)

CREATE TABLE [dbo].[SpellCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalCardTypeId] INT NOT NULL,
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_SpellCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_SpellCardType_OriginalCardTypeId] FOREIGN KEY ([OriginalCardTypeId]) REFERENCES [dbo].[CardTypes] ([Id]),

)

CREATE TABLE [dbo].[TrapCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalCardTypeId] INT NOT NULL,
    [TypeName] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_TrapCardTypeCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TrapCardTypeCardType_OriginalCardTypeId] FOREIGN KEY ([OriginalCardTypeId]) REFERENCES [dbo].[CardTypes] ([Id]),

)

----------------------------------------------------------------------------------

-- Translated
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

CREATE TABLE [dbo].[TranslatedCardTypes]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedCardType_OriginalCardTypeId] FOREIGN KEY ([OriginalCardTypeId]) REFERENCES [dbo].[CardTypes] ([Id]),
	CONSTRAINT [FK_TranslatedCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)

CREATE TABLE [dbo].[TranslatedMonsterCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalMonsterCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedMonsterCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedMonsterCardType_OriginalMonsterCardTypeId] FOREIGN KEY ([OriginalMonsterCardTypeId]) REFERENCES [dbo].[MonsterCardType] ([Id]),
	CONSTRAINT [FK_TranslatedMonsterCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)

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

CREATE TABLE [dbo].[TranslatedSpecialMonsterType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalSpecialMonsterTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedSpecialMonsterTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedSpecialMonsterType_OriginalSpecialMonsterTypeId] FOREIGN KEY ([OriginalSpecialMonsterTypeId]) REFERENCES [dbo].[SpecialMonsterType] ([Id]),
	CONSTRAINT [FK_TranslatedSpecialMonsterType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)

CREATE TABLE [dbo].[TranslatedSpellCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalSpellCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedSpellCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedSpellCardType_OriginalSpellCardTypeId] FOREIGN KEY ([OriginalSpellCardTypeId]) REFERENCES [dbo].[SpellCardType] ([Id]),
	CONSTRAINT [FK_TranslatedSpellCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)

CREATE TABLE [dbo].[TranslatedTrapCardType]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalTrapCardTypeId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
    [TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[ImageExample] NVARCHAR(500) NULL,
	CONSTRAINT [PK_TranslatedTrapCardTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TranslatedTrapCardType_OriginalTrapCardTypeId] FOREIGN KEY ([OriginalTrapCardTypeId]) REFERENCES [dbo].[TrapCardType] ([Id]),
	CONSTRAINT [FK_TranslatedTrapCardType_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
)