using AutoMapper;
using ServerYGO.Data.Entities;

namespace ServerYGO.Automapper
{
    public class YugiMapper : Profile
    {
        public YugiMapper()
        {
            CreateMap<TranslatedCardTypes, CardTypeDetail>().ReverseMap();
            CreateMap<TranslatedAttribute, AttributeDetail>().ReverseMap();
            CreateMap<TranslatedBanlistType, BanlistTypeDetail>().ReverseMap();
            CreateMap<TranslatedMonsterCardType, MonsterCardDetail>().ReverseMap();
            CreateMap<TranslatedMonsterType, MonsterTypeDetail>().ReverseMap();
            CreateMap<TranslatedRarityType, RarityTypeDetail>().ReverseMap();
        }
    }
}
