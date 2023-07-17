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
        }
    }
}
