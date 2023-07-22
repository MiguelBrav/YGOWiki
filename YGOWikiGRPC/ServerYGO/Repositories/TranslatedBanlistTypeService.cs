using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedBanlistTypeService : ITranslatedBanlistTypeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedBanlistTypeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedBanlistType>> GetAllBanlistTypes(string languageId)
        {
            return await _dbContext.TranslatedBanlistType.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedBanlistType> GetBanlistTypeByLanguageId(string languageId, int banlistTypeId)
        {
            return await _dbContext.TranslatedBanlistType.Where(x => x.LanguageId == languageId && x.OriginalBanlistTypeId == banlistTypeId).FirstOrDefaultAsync();
        }
    }
}
