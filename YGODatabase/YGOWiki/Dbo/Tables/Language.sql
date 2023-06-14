CREATE TABLE [dbo].[Language]
(
	[Id] CHAR(5) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_LanguageId] PRIMARY KEY CLUSTERED ([Id] ASC),
)
