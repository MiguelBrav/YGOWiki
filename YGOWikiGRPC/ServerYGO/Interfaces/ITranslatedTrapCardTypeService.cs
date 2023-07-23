using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedTrapCardTypeService
    {
        public Task<List<TranslatedTrapCardType>> GetAllTrapCardsByLanguageId(string languageId);

        public Task<TranslatedTrapCardType> GetTrapCardByLanguageId(string languageId, int trapTypeId);
    }
}
