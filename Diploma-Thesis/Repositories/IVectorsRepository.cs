using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Repositories
{
    public interface IVectorsRepository
    {
        IEnumerable<Vector> GetAllVectors();
        Task<int> AddRange(IEnumerable<Vector> vectors);
    }
}
