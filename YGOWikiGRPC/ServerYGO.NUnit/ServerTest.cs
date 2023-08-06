using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace ServerYGO.NUnit
{
    public class ServerTest
    {
        private GrpcChannel _channel;
        private YGOWiki.YGOWikiClient _client;
        private string _languageES;
        private string _languageEN;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var factory = new WebApplicationFactory<Program>();
            var options = new GrpcChannelOptions { HttpHandler = factory.Server.CreateHandler() };
            _channel = GrpcChannel.ForAddress(factory.Server.BaseAddress, options);
            _languageEN = "en-Us";
            _languageES = "es-Mx";
        }

        [SetUp]
        public void Setup()
        {
            _client = new YGOWiki.YGOWikiClient(_channel);
        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnGeneralCardTypes(string languageId)
        {
            AllTypeCardsReply generalAllCardsTypes = await _client.GetAllTypeCardsAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalAllCardsTypes.CardTypes.Count == 0);
            Assert.That(generalAllCardsTypes.CardTypes.Any());
            Assert.IsNotNull(generalAllCardsTypes);
        }

        [Test]
        [TestCase("es-Mx", 3)]
        [TestCase("en-Us", 3)]
        public async Task ShouldReturnTrapCard(string languageId, int typeCard)
        {
            CardTypeDetail expectedResult = new CardTypeDetail()
            {
                Id = languageId == _languageEN ? 5 : 6,
                OriginalCardTypeId = 3,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Trap Card" : "Carta trampa",
                TranslatedDescription = languageId == _languageEN ? "Cards with purple-colored borders that have various effects. A Trap Card must first be Set and can only be activated after the current turn has finished." +
                " After that, it may be activated during either player's turn." :
                "Son cartas de color púrpura que tienen varios efectos para hacerle difícil las cosas a tu adversario o más fáciles para ti durante un Duelo. Una Carta de Trampa debe ser Colocada en la Zona de Magia y Trampas y, generalmente, sólo puede ser activada después de que el turno actual haya terminado.",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687299218/YGOWiki/TrapCardEN_q3vhwt.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687299219/YGOWiki/TrapCardES_qobrz0.png"

            };

            CardTypeDetail cardType = await _client.GetTypeCardAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = typeCard });

            Assert.IsNotNull(cardType);
            Assert.AreEqual(cardType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnBanlistTypes(string languageId)
        {
            AllBanlistReply generalAllBanlistTypes = await _client.GetAllBanlistAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalAllBanlistTypes.BanlistTypes.Count == 0);
            Assert.That(generalAllBanlistTypes.BanlistTypes.Any());
            Assert.IsNotNull(generalAllBanlistTypes);
        }

        [Test]
        [TestCase("es-Mx", 1)]
        [TestCase("en-Us", 1)]
        public async Task ShouldReturnBanlistCard(string languageId, int banlistId)
        {
            BanlistTypeDetail expectedResult = new BanlistTypeDetail()
            {
                Id = languageId == _languageEN ? 1 : 2,
                OriginalBanlistTypeId = 1,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Forbidden" : "Prohibida",
                TranslatedDescription = languageId == _languageEN ? "Cards that are “Forbidden” cannot be used in your Main Deck, Extra Deck, or Side Deck." :
                "Las cartas Prohibidas no pueden ser utilizadas, en los Decks, Extra Deck y/o Side Deck.",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687279618/YGOWiki/Forbidden_ajt2yg.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687279618/YGOWiki/Forbidden_ajt2yg.png"

            };

            BanlistTypeDetail banlistType = await _client.GetTypeBanlistAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = banlistId });

            Assert.IsNotNull(banlistType);
            Assert.AreEqual(banlistType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnMonsterCardTypes(string languageId)
        {
            AllMonsterCardTypeReply generalAllMonsterCardsTypes = await _client.GetAllMonsterCardTypesAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalAllMonsterCardsTypes.MonsterCardTypes.Count == 0);
            Assert.That(generalAllMonsterCardsTypes.MonsterCardTypes.Any());
            Assert.IsNotNull(generalAllMonsterCardsTypes);
        }


        [Test]
        [TestCase("es-Mx", 8)]
        [TestCase("en-Us", 8)]
        public async Task ShouldReturnLinkMonsterType(string languageId, int monsterCardTypeId)
        {
            MonsterCardDetail expectedResult = new MonsterCardDetail()
            {
                Id = languageId == _languageEN ? 15 : 16,
                OriginalMonsterCardTypeId = 8,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Link monster" : "Monstruo de Enlace",
                TranslatedDescription = languageId == _languageEN ? "The color of their card frame is dark blue, which is similar to that of a Ritual Monster, but with a hexagonal pattern similar" +
                " to a honeycomb structure. Link Monsters have a Link Rating in place of a Level or Rank. A Link Monster's Link Rating determines the total number of Link Materials required to Link " +
                "Summon it. A Link Monster's Link Rating is also equal to the number of Link Arrows it has. " :
                "El color de su marco de carta es azul oscuro, similar al marco de un Monstruo de Ritual, pero con un patrón hexagonal similar a la estructura de un panal. Estas cartas han de estar en" +
                " el Deck Extra. Los Monstruos de Enlace no tienen Niveles y en su lugar tienen un Rating de Enlace. El Rating de Enlace de un Monstruo de Enlace determina el número combinado de monstruos " +
                "Material de Enlace que no sean de Enlace o los Rating de Enlace combinados de los Materiales de Enlace que sean Monstruos de Enlace (o cualquier combinación de los mismos) necesarios para" +
                " Invocar por Enlace. El Rating de Enlace de un Monstruo de Enlace también es igual al número de Flechas de Enlace que tiene. ",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687371105/YGOWiki/LinkMonsterEN_gvsfov.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687371105/YGOWiki/LinkMonsterES_ws6unj.png"

            };

            MonsterCardDetail monsterCardType = await _client.GetMonsterCardTypeAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = monsterCardTypeId });

            Assert.IsNotNull(monsterCardType);
            Assert.AreEqual(monsterCardType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnMonsterTypes(string languageId)
        {
            AllMonsterTypeReply generalMonsterTypes = await _client.GetAllMonsterTypesAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalMonsterTypes.MonsterTypes.Count == 0);
            Assert.That(generalMonsterTypes.MonsterTypes.Any());
            Assert.IsNotNull(generalMonsterTypes);
        }

        [Test]
        [TestCase("es-Mx", 5)]
        [TestCase("en-Us", 5)]
        public async Task ShouldReturnCyberseType(string languageId, int monsterTypeId)
        {
            MonsterTypeDetail expectedResult = new MonsterTypeDetail()
            {
                Id = languageId == _languageEN ? 9 : 10,
                OriginalMonsterTypeId = 5,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Cyberse" : "Ciberso",
                TranslatedDescription = languageId == _languageEN ? "Appearance-wise, they resemble creatures and humanoids strongly connected to elements of digital technology and cyberspace. " :
                "Los monstruos Ciberso se caracterizan por ser, en su mayoría, monstruos con apariencia cibernética, tecnológica o virtual. También suelen tener nombres alusivos a dichos conceptos.",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687373481/YGOWiki/Cyberse-MD_ygwvfd.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687373481/YGOWiki/Cyberse-MD_ygwvfd.png"

            };

            MonsterTypeDetail monsterType = await _client.GetMonsterTypeAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = monsterTypeId });

            Assert.IsNotNull(monsterType);
            Assert.AreEqual(monsterType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnAttributes(string languageId)
        {
            AllAttributeReply generalAttributes = await _client.GettAllAttributesAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalAttributes.Attributes.Count == 0);
            Assert.That(generalAttributes.Attributes.Any());
            Assert.IsNotNull(generalAttributes);
        }


        [Test]
        [TestCase("es-Mx", 2)]
        [TestCase("en-Us", 2)]
        public async Task ShouldReturnDivineAttribute(string languageId, int attributeId)
        {
            AttributeDetail expectedResult = new AttributeDetail()
            {
                Id = languageId == _languageEN ? 3 : 4,
                OriginalAttributeId = 2,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Divine Attribute" : "Atributo Divino",
                TranslatedDescription = languageId == _languageEN ? "The DIVINE Attribute was used on the three original Egyptian God cards: Obelisk the Tormentor, Slifer the Sky Dragon and The Winged Dragon of Ra." :
                "La familia más conocida con este Atributo son los monstruos Dios Egipcio: Obelisco el Atormentador, Slifer el Dragón del Cielo y El Dragón Alado de Ra.",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687069358/YGOWiki/DivineEN_yidsq7.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687069388/YGOWiki/DivineES_juzgmt.png"

            };

            AttributeDetail attributeType = await _client.GetAttributeAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = attributeId });

            Assert.IsNotNull(attributeType);
            Assert.AreEqual(attributeType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnRarities(string languageId)
        {
            AllRarityReply generalRarities = await _client.GetAllRaritiesAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalRarities.Rarities.Count == 0);
            Assert.That(generalRarities.Rarities.Any());
            Assert.IsNotNull(generalRarities);
        }

        [Test]
        [TestCase("es-Mx", 1)]
        [TestCase("en-Us", 1)]
        public async Task ShouldReturnCommonRarity(string languageId, int attributeId)
        {
            RarityTypeDetail expectedResult = new RarityTypeDetail()
            {
                Id = languageId == _languageEN ? 1 : 2,
                OriginalRarityTypeId = 1,
                LanguageId = languageId,
                TranslatedDescription = languageId == _languageEN ? "Common" : "Común",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687459843/YGOWiki/CommonCard_jkuict.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687459843/YGOWiki/CommonCard_jkuict.png"

            };

            RarityTypeDetail rarityType = await _client.GetRarityTypeAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = attributeId });

            Assert.IsNotNull(rarityType);
            Assert.AreEqual(rarityType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnSpecialMonsters(string languageId)
        {
            AllSpecialMonsterTypeReply generalSpecialMonsters = await _client.GetAllSpecialMonstersAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalSpecialMonsters.SpecialMonsterTypes.Count == 0);
            Assert.That(generalSpecialMonsters.SpecialMonsterTypes.Any());
            Assert.IsNotNull(generalSpecialMonsters);
        }

        [Test]
        [TestCase("es-Mx", 1)]
        [TestCase("en-Us", 1)]
        public async Task ShouldReturnGemini(string languageId, int attributeId)
        {
            SpecialMonsterTypeDetail expectedResult = new SpecialMonsterTypeDetail()
            {
                Id = languageId == _languageEN ? 1 : 2,
                OriginalSpecialMonsterTypeId = 1,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Gemini" : "Géminis",
                TranslatedDescription = languageId == _languageEN ? "They are treated as Normal Monsters on the field and in the GY, and can gain their effects by performing an additional " +
                "Normal Summon on them while they are face-up on the field." :
                " Comparten la característica de ser tratados como Monstruos Normales cuando se encuentran en el Campo o el Cementerio, además de tener efectos que deben de ser \"desbloqueados\" " +
                "al realizar una segunda Invocación Normal mientras están boca arriba en el Campo.",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687461144/YGOWiki/GeminiEN_fgbuya.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687461146/YGOWiki/GeminiES_vddhoj.png"

            };

            SpecialMonsterTypeDetail specialMonsterType = await _client.GetSpecialMonsterTypeAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = attributeId });

            Assert.IsNotNull(specialMonsterType);
            Assert.AreEqual(specialMonsterType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnSpellsCardsType(string languageId)
        {
            AllSpellTypeReply generalSpells = await _client.GetAllSpellCardsAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalSpells.SpellTypes.Count == 0);
            Assert.That(generalSpells.SpellTypes.Any());
            Assert.IsNotNull(generalSpells);
        }

        [Test]
        [TestCase("es-Mx", 6)]
        [TestCase("en-Us", 6)]
        public async Task ShouldReturnQuickPlaySpell(string languageId, int attributeId)
        {
            SpellTypeDetail expectedResult = new SpellTypeDetail()
            {
                Id = languageId == _languageEN ? 11 : 12,
                OriginalSpellCardTypeId = 6,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Quick-play Spell Card" : "Carta Mágica de Juego Rápido",
                TranslatedDescription = languageId == _languageEN ? "Which have a lightning bolt symbol. A type of Spell Card that are Spell Speed 2. The turn player can activate Quick-Play " +
                "Spell Cards from their hand during any Phase of their turn; either player can activate Set Quick-Play Spell cards during any Phase in either player's turn, except during the turn they are Set." :
                "Tiene un icono de rayo al lado del nombre. Son una clase de Carta Mágica con una Velocidad de Hechizo 2 que se pueden activar durante cualquier Fase de tu turno, así como en" +
                " el de tu adversario (siempre que se hubiese Colocado previamente), similar a las Cartas de Trampa.",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687675458/QuickPlaySpellEN_lmiuad.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687675458/QuickPlaySpellES_em86lj.png"

            };

            SpellTypeDetail spellType = await _client.GetSpellTypeAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = attributeId });

            Assert.IsNotNull(spellType);
            Assert.AreEqual(spellType, expectedResult);

        }

        [Test]
        [TestCase("es-Mx")]
        [TestCase("en-Us")]
        public async Task ShouldReturnTrapCardsType(string languageId)
        {
            AllTrapTypeReply generalTraps = await _client.GetAllTrapCardsAsync(new ByLanguageId { LanguageId = languageId });

            Assert.False(generalTraps.TrapTypes.Count == 0);
            Assert.That(generalTraps.TrapTypes.Any());
            Assert.IsNotNull(generalTraps);
        }

        [Test]
        [TestCase("es-Mx", 3)]
        [TestCase("en-Us", 3)]
        public async Task ShouldReturnCounterTrap(string languageId, int attributeId)
        {
            TrapTypeDetail expectedResult = new TrapTypeDetail()
            {
                Id = languageId == _languageEN ? 5 : 6,
                OriginalTrapCardTypeId = 3,
                LanguageId = languageId,
                TranslatedName = languageId == _languageEN ? "Counter Trap Card" : "Carta Trampa de Contraefecto",
                TranslatedDescription = languageId == _languageEN ? "Which have a curved arrow symbol. are a unique Trap card type that are of Spell Speed 3. Being the only cards/effects that are" +
                " Spell Speed 3, only other Counter Trap Cards can be activated in response to them. Most of them can only be activated to negate or punish the activations of other cards, or Summons of monsters." :
                "Tiene un icono de flecha curva al lado del nombre. son las más veloces en cuanto a Velocidad de Hechizo y pueden responder a la activación de cualquier carta si no contradicen el" +
                " texto en la descripción de la misma. Esta clase de cartas se usa comúnmente para la cancelación de efectos de otras cartas, ya sean tanto Cartas Mágicas o de Trampa, como para la" +
                " activación de efectos o Invocación de monstruos.",
                ImageExample = languageId == _languageEN ? "https://res.cloudinary.com/imgresd/image/upload/v1687676988/CounterTrapEN_uorvfq.png" :
                "https://res.cloudinary.com/imgresd/image/upload/v1687676988/CounterTrapES_pmny3q.png"

            };

            TrapTypeDetail trapType = await _client.GetTrapTypeAsync(new ByLanguageIdAndId { LanguageId = languageId, Id = attributeId });

            Assert.IsNotNull(trapType);
            Assert.AreEqual(trapType, expectedResult);

        }
    }
}