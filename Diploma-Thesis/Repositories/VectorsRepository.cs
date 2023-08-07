using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Repositories
{
    public class VectorsRepository : IVectorsRepository
    {
        private readonly Context _db;

        public VectorsRepository(Context db)
        {
            _db = db;
        }

        public IEnumerable<Vector> GetAllVectors()
        {
            return _db.Vectors;
        }
        public async Task<int> AddRange(IEnumerable<Vector> vectors)
        {
            await _db.Vectors.AddRangeAsync(vectors);
            return await _db.SaveChangesAsync();
        }
    }
}
