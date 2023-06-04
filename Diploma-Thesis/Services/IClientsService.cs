using Diploma_Thesis.Models;

namespace Diploma_Thesis.Services
{
    public interface IClientsService
    {
        IEnumerable<ClientSimpleModel> GetAllClients();
        Task<ClientDetailedModel> GetClientDetailedAsync(Guid id);
        Task<int> AddClientAsync(ClientDetailedModel model);
        Task<int> UpdateAsync(ClientDetailedModel model);
    }
}
