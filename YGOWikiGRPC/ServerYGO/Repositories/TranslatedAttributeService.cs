using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedAttributeService : ITranslatedAttributeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedAttributeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedAttribute>> GetAllAttributesByLanguageId(string languageId)
        {
           return await _dbContext.TranslatedAttribute.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedAttribute> GetAttributeByLanguageId(string languageId, int attributeId)
        {
            return await _dbContext.TranslatedAttribute.Where(x => x.LanguageId == languageId && x.OriginalAttributeId == attributeId).FirstOrDefaultAsync();
        }

    }
}
