using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedTrapCardTypeService : ITranslatedTrapCardTypeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedTrapCardTypeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedTrapCardType>> GetAllTrapCardsByLanguageId(string languageId)
        {
            return await _dbContext.TranslatedTrapCardType.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedTrapCardType> GetTrapCardByLanguageId(string languageId, int trapTypeId)
        {
            return await _dbContext.TranslatedTrapCardType.Where(x => x.LanguageId == languageId && x.OriginalTrapCardTypeId == trapTypeId).FirstOrDefaultAsync();
        }
    }
}
