using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedMonsterTypeService
    {
        public Task<List<TranslatedMonsterType>> GetAllMonsterByLanguageId (string languageId);

        public Task<TranslatedMonsterType> GetMonsterByLanguageId(string languageId, int monsterTypeId);
    }
}
