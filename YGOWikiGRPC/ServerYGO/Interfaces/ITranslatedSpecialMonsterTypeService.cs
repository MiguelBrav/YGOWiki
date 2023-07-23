using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedSpecialMonsterTypeService
    {
        public Task<List<TranslatedSpecialMonsterType>> GetAllSpecialMonsterByLanguageId(string languageId);

        public Task<TranslatedSpecialMonsterType> GetSpecialMonsterByLanguageId(string languageId, int monsterTypeId);
    }
}
