using Diploma_Thesis.Entities;
using Diploma_Thesis.Models;

namespace Diploma_Thesis.Services
{
    public interface IExpertisesService
    {
        Task<int> AddAsync(ExpertiseModel model);
        IEnumerable<ExpertiseModel> GetByClientId(Guid clientId);
        Task<ExpertiseModel> AnalyzePhoto(IFormFile photo, Guid clientId);
        Task<int> UpdateAsync(ExpertiseModel model);
        Task<int> GenerateTestVectors();
        Vector GetRandomVector();
        IEnumerable<ExpertiseModel> AnalyseAllVectors();
    }
}
