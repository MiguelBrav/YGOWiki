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
        private readonly ITranslatedBanlistTypeService _banlistTypeService;
        private readonly ITranslatedMonsterCardTypeService _monsterTypeService;
        private readonly ITranslatedMonsterTypeService _monsterService;
        private readonly ITranslatedRarityTypeService _rarityService;
        private readonly ITranslatedSpecialMonsterTypeService _specialMonsterService;
        private readonly ITranslatedSpellCardTypeService _spellService;
        private readonly ITranslatedTrapCardTypeService _trapService;
        private readonly IMapper _mapper;

        public YGOService(ITranslatedCardTypesService cardTypesService, IMapper mapper, ITranslatedAttributeService attributeService, ITranslatedBanlistTypeService banlistTypeService, ITranslatedMonsterCardTypeService monsterTypeService, ITranslatedMonsterTypeService monsterService, ITranslatedRarityTypeService rarityService, ITranslatedSpecialMonsterTypeService specialMonsterService, ITranslatedSpellCardTypeService spellService, ITranslatedTrapCardTypeService trapService)
        {
            _cardTypesService = cardTypesService;
            _mapper = mapper;
            _attributeService = attributeService;
            _banlistTypeService = banlistTypeService;
            _monsterTypeService = monsterTypeService;
            _monsterService = monsterService;
            _rarityService = rarityService;
            _specialMonsterService = specialMonsterService;
            _spellService = spellService;
            _trapService = trapService;
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

        public async override Task<AllBanlistReply> GetAllBanlist(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedBanlistType> banlistTypes = await _banlistTypeService.GetAllBanlistTypes(request.LanguageId);

            AllBanlistReply result = new AllBanlistReply();

            List<BanlistTypeDetail> results = _mapper.Map<List<TranslatedBanlistType>, List<BanlistTypeDetail>>(banlistTypes);

            result.BanlistTypes.AddRange(results);

            return result;
        }

        public async override Task<BanlistTypeDetail> GetTypeBanlist(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedBanlistType cardAttribute = await _banlistTypeService.GetBanlistTypeByLanguageId(request.LanguageId, (int)request.Id);

            BanlistTypeDetail result = _mapper.Map<BanlistTypeDetail>(cardAttribute);

            return result;
        }


        public async override Task<AllMonsterCardTypeReply> GetAllMonsterCardTypes(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedMonsterCardType> monsterCardTypes = await _monsterTypeService.GetAllTypeMonsterByLanguageId(request.LanguageId);

            AllMonsterCardTypeReply result = new AllMonsterCardTypeReply();

            List<MonsterCardDetail> results = _mapper.Map<List<TranslatedMonsterCardType>, List<MonsterCardDetail>>(monsterCardTypes);

            result.MonsterCardTypes.AddRange(results);

            return result;
        }

        public async override Task<MonsterCardDetail> GetMonsterCardType(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedMonsterCardType monsterCardType = await _monsterTypeService.GetMonsterTypeByLanguageId(request.LanguageId, (int)request.Id);

            MonsterCardDetail result = _mapper.Map<MonsterCardDetail>(monsterCardType);

            return result;
        }

        public async override Task<AllMonsterTypeReply> GetAllMonsterTypes(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedMonsterType> monsterTypes = await _monsterService.GetAllMonsterByLanguageId(request.LanguageId);

            AllMonsterTypeReply result = new AllMonsterTypeReply();

            List<MonsterTypeDetail> results = _mapper.Map<List<TranslatedMonsterType>, List<MonsterTypeDetail>>(monsterTypes);

            result.MonsterTypes.AddRange(results);

            return result;
        }

        public async override Task<MonsterTypeDetail> GetMonsterType(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedMonsterType monsterType = await _monsterService.GetMonsterByLanguageId(request.LanguageId, (int)request.Id);

            MonsterTypeDetail result = _mapper.Map<MonsterTypeDetail>(monsterType);

            return result;
        }

        public async override Task<AllRarityReply> GetAllRarities(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedRarityType> rarityTypes = await _rarityService.GetAllRaritiesByLanguageId(request.LanguageId);

            AllRarityReply result = new AllRarityReply();

            List<RarityTypeDetail> results = _mapper.Map<List<TranslatedRarityType>, List<RarityTypeDetail>>(rarityTypes);

            result.Rarities.AddRange(results);

            return result;
        }

        public async override Task<RarityTypeDetail> GetRarityType(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedRarityType rarityType = await _rarityService.GetRarityTypeByLanguageId(request.LanguageId, (int)request.Id);

            RarityTypeDetail result = _mapper.Map<RarityTypeDetail>(rarityType);

            return result;
        }

        public async override Task<AllSpecialMonsterTypeReply> GetAllSpecialMonsters(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedSpecialMonsterType> specialMonsterTypes = await _specialMonsterService.GetAllSpecialMonsterByLanguageId(request.LanguageId);

            AllSpecialMonsterTypeReply result = new AllSpecialMonsterTypeReply();

            List<SpecialMonsterTypeDetail> results = _mapper.Map<List<TranslatedSpecialMonsterType>, List<SpecialMonsterTypeDetail>>(specialMonsterTypes);

            result.SpecialMonsterTypes.AddRange(results);

            return result;
        }

        public async override Task<SpecialMonsterTypeDetail> GetSpecialMonsterType(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedSpecialMonsterType specialMonsterType = await _specialMonsterService.GetSpecialMonsterByLanguageId(request.LanguageId, (int)request.Id);

            SpecialMonsterTypeDetail result = _mapper.Map<SpecialMonsterTypeDetail>(specialMonsterType);

            return result;
        }

        public async override Task<AllSpellTypeReply> GetAllSpellCards(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedSpellCardType> spellTypes = await _spellService.GetAllSpellCardsByLanguageId(request.LanguageId);

            AllSpellTypeReply result = new AllSpellTypeReply();

            List<SpellTypeDetail> results = _mapper.Map<List<TranslatedSpellCardType>, List<SpellTypeDetail>>(spellTypes);

            result.SpellTypes.AddRange(results);

            return result;
        }

        public async override Task<SpellTypeDetail> GetSpellType(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedSpellCardType spellType = await _spellService.GetSpellCardByLanguageId(request.LanguageId, (int)request.Id);

            SpellTypeDetail result = _mapper.Map<SpellTypeDetail>(spellType);

            return result;
        }

        public async override Task<AllTrapTypeReply> GetAllTrapCards(ByLanguageId request, ServerCallContext context)
        {
            List<TranslatedTrapCardType> trapTypes = await _trapService.GetAllTrapCardsByLanguageId(request.LanguageId);

            AllTrapTypeReply result = new AllTrapTypeReply();

            List<TrapTypeDetail> results = _mapper.Map<List<TranslatedTrapCardType>, List<TrapTypeDetail>>(trapTypes);

            result.TrapTypes.AddRange(results);

            return result;
        }

        public async override Task<TrapTypeDetail> GetTrapType(ByLanguageIdAndId request, ServerCallContext context)
        {
            TranslatedTrapCardType trapType = await _trapService.GetTrapCardByLanguageId(request.LanguageId, (int)request.Id);

            TrapTypeDetail result = _mapper.Map<TrapTypeDetail>(trapType);

            return result;
        }
    }
}