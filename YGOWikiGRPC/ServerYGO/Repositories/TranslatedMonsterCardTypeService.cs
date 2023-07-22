using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedMonsterCardTypeService : ITranslatedMonsterCardTypeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedMonsterCardTypeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedMonsterCardType>> GetAllTypeMonsterByLanguageId(string languageId)
        {
            return await _dbContext.TranslatedMonsterCardType.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedMonsterCardType> GetMonsterTypeByLanguageId(string languageId, int monsterTypeId)
        {
            return await _dbContext.TranslatedMonsterCardType.Where(x => x.LanguageId == languageId && x.OriginalMonsterCardTypeId == monsterTypeId).FirstOrDefaultAsync();
        }
    }
}
