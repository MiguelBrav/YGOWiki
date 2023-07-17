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
        private readonly ITranslatedAttributeService _attributeService;
        private readonly IMapper _mapper;

        public YGOService(ITranslatedCardTypesService cardTypesService, IMapper mapper, ITranslatedAttributeService attributeService)
        {
            _cardTypesService = cardTypesService;
            _mapper = mapper;
            _attributeService = attributeService;
        }

        public async override Task<AllTypeCardsReply> GetAllTypeCards(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedCardTypes> cardTypes = await _cardTypesService.GetAllTypeCardsByLanguageId(request.LanguageId);

            AllTypeCardsReply result = new AllTypeCardsReply();

            List<CardTypeDetail> results = _mapper.Map<List<TranslatedCardTypes>, List<CardTypeDetail>>(cardTypes);

            result.CardTypes.AddRange(results);

            return result;
        }

        public async override Task<CardTypeDetail> GetTypeCard(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedCardTypes cardType = await _cardTypesService.GetCardTypeByLanguageId(request.LanguageId, (int)request.Id);

            CardTypeDetail result = _mapper.Map<CardTypeDetail>(cardType);

            return result;
        }

        public async override Task<AllAttributeReply> GettAllAttributes(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedAttribute> cardAttributes = await _attributeService.GetAllAttributesByLanguageId(request.LanguageId);

            AllAttributeReply result = new AllAttributeReply();

            List<AttributeDetail> results = _mapper.Map<List<TranslatedAttribute>, List<AttributeDetail>>(cardAttributes);

            result.Attributes.AddRange(results);

            return result;
        }

        public async override Task<AttributeDetail> GetAttribute(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedAttribute cardAttribute = await _attributeService.GetAttributeByLanguageId(request.LanguageId, (int)request.Id);

            AttributeDetail result = _mapper.Map<AttributeDetail>(cardAttribute);

            return result;
        }

    }
}