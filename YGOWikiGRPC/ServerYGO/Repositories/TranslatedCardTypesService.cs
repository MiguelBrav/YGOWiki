using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedCardTypesService : ITranslatedCardTypesService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedCardTypesService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedCardTypes>> GetAllTypeCardsByLanguageId(string languageId)
        {
           return await _dbContext.TranslatedCardTypes.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedCardTypes> GetCardTypeByLanguageId(string languageId, int typeCardId)
        {
            return await _dbContext.TranslatedCardTypes.Where(x => x.LanguageId == languageId && x.OriginalCardTypeId == typeCardId).FirstOrDefaultAsync();
        }
    }
}
