using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedRarityTypeService : ITranslatedRarityTypeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedRarityTypeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedRarityType>> GetAllRaritiesByLanguageId(string languageId)
        {
            return await _dbContext.TranslatedRarityType.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedRarityType> GetRarityTypeByLanguageId(string languageId, int rarityTypeId)
        {
            return await _dbContext.TranslatedRarityType.Where(x => x.LanguageId == languageId && x.OriginalRarityTypeId == rarityTypeId).FirstOrDefaultAsync();
        }
    }
}
