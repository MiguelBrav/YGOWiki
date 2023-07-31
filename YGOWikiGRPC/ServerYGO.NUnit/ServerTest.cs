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
        [TestCase("es-Mx",3)]
        [TestCase("en-Us",3)]
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

            CardTypeDetail cardType = await _client.GetTypeCardAsync(new ByLanguageIdAndId { LanguageId = languageId , Id = typeCard});

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
    }
}