using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Repositories
{
    public class ExpertisesRepository : IExpertisesRepository
    {
        private readonly Context _db;

        public ExpertisesRepository(Context db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Expertise expertise)
        {
            await _db.Expertises.AddAsync(expertise);
            return await _db.SaveChangesAsync();
        }

        public IEnumerable<Expertise> GetByClientId(Guid clientId) 
        {
            return _db.Expertises.Where(x => x.ClientId == clientId);
        }
        public async Task<int> UpdateAsync(Expertise expertise)
        {
            _db.Expertises.Update(expertise);
            return await _db.SaveChangesAsync();
        }
    }
}
