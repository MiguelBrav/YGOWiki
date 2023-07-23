using Microsoft.EntityFrameworkCore;
using ServerYGO.Data;
using ServerYGO.Data.Entities;
using ServerYGO.Interfaces;

namespace ServerYGO.Repositories
{
    public class TranslatedSpellCardTypeService : ITranslatedSpellCardTypeService
    {
        private readonly DbContextClass _dbContext;

        public TranslatedSpellCardTypeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TranslatedSpellCardType>> GetAllSpellCardsByLanguageId(string languageId)
        {
            return await _dbContext.TranslatedSpellCardType.Where(x => x.LanguageId == languageId).ToListAsync();
        }

        public async Task<TranslatedSpellCardType> GetSpellCardByLanguageId(string languageId, int spellTypeId)
        {
            return await _dbContext.TranslatedSpellCardType.Where(x => x.LanguageId == languageId && x.OriginalSpellCardTypeId == spellTypeId).FirstOrDefaultAsync();

        }
    }
}
