using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Repositories
{
    public interface IExpertisesRepository
    {
        Task<int> AddAsync(Expertise expertise);
        IEnumerable<Expertise> GetByClientId(Guid clientId);
    }
}
