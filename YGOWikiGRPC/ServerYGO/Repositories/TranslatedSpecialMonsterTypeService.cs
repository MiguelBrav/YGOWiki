using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedSpecialMonsterTypeService : ITranslatedSpecialMonsterTypeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedSpecialMonsterTypeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedSpecialMonsterType>> GetAllSpecialMonsterByLanguageId(string languageId)
        {
            return await _dbContext.TranslatedSpecialMonsterType.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedSpecialMonsterType> GetSpecialMonsterByLanguageId(string languageId, int monsterTypeId)
        {
            return await _dbContext.TranslatedSpecialMonsterType.Where(x => x.LanguageId == languageId && x.OriginalSpecialMonsterTypeId == monsterTypeId).FirstOrDefaultAsync();
        }
    }
}
