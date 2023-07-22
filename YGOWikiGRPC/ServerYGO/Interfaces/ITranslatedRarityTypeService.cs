using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedMonsterCardTypeService
    {
        public Task<List<TranslatedMonsterCardType>> GetAllTypeMonsterByLanguageId (string languageId);

        public Task<TranslatedMonsterCardType> GetMonsterTypeByLanguageId(string languageId, int rarityTypeId);
    }
}
