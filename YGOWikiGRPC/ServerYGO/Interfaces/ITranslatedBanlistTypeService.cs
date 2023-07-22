using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedBanlistTypeService
    {
        public Task<List<TranslatedBanlistType>> GetAllBanlistTypes(string languageId);

        public Task<TranslatedBanlistType> GetBanlistTypeByLanguageId(string languageId, int banlistTypeId);
    }
}
