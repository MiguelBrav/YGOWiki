using ServerYGO.Data.Entities;

namespace ServerYGO.Interfaces
{
    public interface ITranslatedAttributeService
    {
        public Task<List<TranslatedAttribute>> GetAllAttributesByLanguageId (string languageId);

        public Task<TranslatedAttribute> GetAttributeByLanguageId(string languageId, int attributeId);
    }
}
