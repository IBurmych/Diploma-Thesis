using Diploma_Thesis.Models;

namespace Diploma_Thesis.Services
{
    public interface IExpertisesService
    {
        Task<int> AddAsync(ExpertiseModel model);
        IEnumerable<ExpertiseModel> GetByClientId(Guid clientId);
    }
}
