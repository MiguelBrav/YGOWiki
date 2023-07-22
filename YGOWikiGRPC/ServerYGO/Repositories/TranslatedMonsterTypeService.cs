using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedMonsterTypeService : ITranslatedMonsterTypeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedMonsterTypeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedMonsterType>> GetAllMonsterByLanguageId(string languageId)
        {
            return await _dbContext.TranslatedMonsterType.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedMonsterType> GetMonsterByLanguageId(string languageId, int monsterTypeId)
        {
            return await _dbContext.TranslatedMonsterType.Where(x => x.LanguageId == languageId && x.OriginalMonsterTypeId == monsterTypeId).FirstOrDefaultAsync();
        }


    }
}
