using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedRarityTypeService
    {
        public Task<List<TranslatedRarityType>> GetAllRaritiesByLanguageId (string languageId);

        public Task<TranslatedRarityType> GetRarityTypeByLanguageId(string languageId, int monsterTypeId);
    }
}
