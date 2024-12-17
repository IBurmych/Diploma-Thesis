using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Repositories
{
    public interface IDiapasonsRepository
    {
        Task<int> AddAsync(Diapason diapason);
        Task<List<Diapason>> GetAllAsync();
        Task<int> AddRangeAsync(IEnumerable<Diapason> diapasons);
        Task<int> UpdateRange(List<Diapason> diapasons);
    }
}
