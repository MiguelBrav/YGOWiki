using AutoMapper;
using Grpc.Core;
using ServerYGO;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Services
{
    public class YGOService : YGOWiki.YGOWikiBase
    {
        private readonly ITranslatedCardTypesService _cardTypesService;
        private readonly IMapper _mapper;

        public YGOService(ITranslatedCardTypesService cardTypesService, IMapper mapper)
        {
            _cardTypesService = cardTypesService;
            _mapper = mapper;
        }

        public async override Task<AllTypeCardsReply> GetAllTypeCards(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedCardTypes> cardTypes = await _cardTypesService.GetAllTypeCardsByLanguageId(request.LanguageId);

            AllTypeCardsReply result = new AllTypeCardsReply();

            List<CardTypeDetail> results = _mapper.Map<List<TranslatedCardTypes>, List<CardTypeDetail>>(cardTypes);

            result.CardTypes.AddRange(results);

            return result;
        }

    }
}