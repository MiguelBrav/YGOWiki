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

    }
}