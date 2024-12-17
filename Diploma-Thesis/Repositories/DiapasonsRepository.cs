using Diploma_Thesis.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma_Thesis.Repositories
{
    public class DiapasonsRepository : IDiapasonsRepository
    {
        private readonly Context _db;

        public DiapasonsRepository(Context db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Diapason diapason)
        {
            await _db.Diapasons.AddAsync(diapason);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> AddRangeAsync(IEnumerable<Diapason> diapasons)
        {
            await _db.Diapasons.AddRangeAsync(diapasons);
            return await _db.SaveChangesAsync();
        }
        public async Task<List<Diapason>> GetAllAsync()
        {
            return await _db.Diapasons.ToListAsync();
        }
        public async Task<int> UpdateRange(List<Diapason> diapasons)
        {
            _db.Diapasons.UpdateRange(diapasons);
            return await _db.SaveChangesAsync();
        }
    }
}
