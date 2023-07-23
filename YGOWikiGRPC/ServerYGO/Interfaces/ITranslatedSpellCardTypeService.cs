using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedSpellCardTypeService
    {
        public Task<List<TranslatedSpellCardType>> GetAllSpellCardsByLanguageId(string languageId);

        public Task<TranslatedSpellCardType> GetSpellCardByLanguageId(string languageId, int spellTypeId);
    }
}
