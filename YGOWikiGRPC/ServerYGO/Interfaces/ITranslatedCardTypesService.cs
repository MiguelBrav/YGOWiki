using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedCardTypesService
    {
        public Task<List<TranslatedCardTypes>> GetAllTypeCardsByLanguageId (string languageId);

        public Task<TranslatedCardTypes> GetCardTypeByLanguageId(string languageId, int typeCardId);
    }
}
