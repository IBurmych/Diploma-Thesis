using Diploma_Thesis.Models;

namespace Diploma_Thesis.Services
{
    public interface IDiapasonService
    {
        DiapasonСrossingModel GetСrossing(DiapasonModel firstDiapason, DiapasonModel secondDiapason);
        Task<IEnumerable<FullDiapasonModel>> SaveDiapasons(List<FullDiapasonModel> diapasons);
        Task<IEnumerable<FullDiapasonModel>> GetAllAsync();
    }
}
