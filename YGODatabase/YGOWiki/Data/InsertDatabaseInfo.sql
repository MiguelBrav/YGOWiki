--dbo.Attribute
INSERT INTO [dbo].[Attribute] ([TypeName]) VALUES ('Dark')
INSERT INTO [dbo].[Attribute] ([TypeName]) VALUES ('Divine')
INSERT INTO [dbo].[Attribute] ([TypeName]) VALUES ('Earth')
INSERT INTO [dbo].[Attribute] ([TypeName]) VALUES ('Fire')
INSERT INTO [dbo].[Attribute] ([TypeName]) VALUES ('Light')
INSERT INTO [dbo].[Attribute] ([TypeName]) VALUES ('Water')
INSERT INTO [dbo].[Attribute] ([TypeName]) VALUES ('Wind')

--dbo.BanlistType
INSERT INTO [dbo].[BanlistType] ([TypeName]) VALUES ('Forbidden')
INSERT INTO [dbo].[BanlistType] ([TypeName]) VALUES ('Limited')
INSERT INTO [dbo].[BanlistType] ([TypeName]) VALUES ('Semi-limited')
INSERT INTO [dbo].[BanlistType] ([TypeName]) VALUES ('Unlimited')

--dbo.CardTypes

INSERT INTO [dbo].[CardTypes] ([TypeName]) VALUES ('Monster Card')
INSERT INTO [dbo].[CardTypes] ([TypeName]) VALUES ('Spell Card')
INSERT INTO [dbo].[CardTypes] ([TypeName]) VALUES ('Trap Card')

--dbo.Language
INSERT INTO [dbo].[Language] ([Id] ,[Description]) VALUES ('en-US','English Language')
INSERT INTO [dbo].[Language] ([Id] ,[Description]) VALUES ('es-Mx','Spanish Language')

--dbo.MonsterCardType
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Normal')
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Effect')
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Fusion')
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Ritual')
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Synchro')
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Xyz')
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Pendulum')
INSERT INTO [dbo].[MonsterCardType] ([OriginalCardTypeId],[TypeName]) VALUES (1,'Link')

--dbo.MonsterType
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Aqua')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Beast')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Beast-Warrior')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Creator God')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Cyberse')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Dinosaur')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Divine-Beast')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Dragon')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Fairy')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Fiend')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Fish')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Insect')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Machine')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Plant')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Psychic')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Pyro')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Reptile')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Rock')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Sea Serpent')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Spellcaster')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Thunder')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Warrior')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Winged Beast')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Wyrm')
INSERT INTO [dbo].[MonsterType] ([TypeName]) VALUES ('Zombie')

--dbo.RarityType
INSERT INTO [dbo].[RarityType] ([Description]) VALUES ('Common')
INSERT INTO [dbo].[RarityType] ([Description]) VALUES ('Rare')
INSERT INTO [dbo].[RarityType] ([Description]) VALUES ('Super Rare')
INSERT INTO [dbo].[RarityType] ([Description]) VALUES ('Ultra Rare')
INSERT INTO [dbo].[RarityType] ([Description]) VALUES ('Secret Rare')
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Ultimate Rare',1)
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Ghost Rare',1)
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Gold Rare',1)
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Starfoil Rare',1)
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Mosaic Rare',1)
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Shatterfoil Rare',1)
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Quarter Century Secret Rare',1)
INSERT INTO [dbo].[RarityType] ([Description],[IsSpecial]) VALUES ('Collector''s',1)

--dbo.SpecialMonsterType
INSERT INTO [dbo].[SpecialMonsterType] ([TypeName]) VALUES ('Gemini')
INSERT INTO [dbo].[SpecialMonsterType] ([TypeName]) VALUES ('Union')
INSERT INTO [dbo].[SpecialMonsterType] ([TypeName]) VALUES ('Spirit')
INSERT INTO [dbo].[SpecialMonsterType] ([TypeName]) VALUES ('Tuner')
INSERT INTO [dbo].[SpecialMonsterType] ([TypeName]) VALUES ('Flip')

--dbo.SpellCardType
INSERT INTO  [dbo].[SpellCardType] ([OriginalCardTypeId],[TypeName]) VALUES (2,'Normal')
INSERT INTO  [dbo].[SpellCardType] ([OriginalCardTypeId],[TypeName]) VALUES (2,'Continuous')
INSERT INTO  [dbo].[SpellCardType] ([OriginalCardTypeId],[TypeName]) VALUES (2,'Equip')
INSERT INTO  [dbo].[SpellCardType] ([OriginalCardTypeId],[TypeName]) VALUES (2,'Field')
INSERT INTO  [dbo].[SpellCardType] ([OriginalCardTypeId],[TypeName]) VALUES (2,'Ritual')
INSERT INTO  [dbo].[SpellCardType] ([OriginalCardTypeId],[TypeName]) VALUES (2,'Quick-play')

--dbo.TrapCardType
INSERT INTO  [dbo].[TrapCardType] ([OriginalCardTypeId],[TypeName]) VALUES (3,'Normal')
INSERT INTO  [dbo].[TrapCardType] ([OriginalCardTypeId],[TypeName]) VALUES (3,'Continuous')
INSERT INTO  [dbo].[TrapCardType] ([OriginalCardTypeId],[TypeName]) VALUES (3,'Counter')

