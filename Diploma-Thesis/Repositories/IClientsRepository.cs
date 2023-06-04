using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Repositories
{
    public interface IClientsRepository
    {
        Task<int> AddAsync(Client client);
        IEnumerable<Client> GetAll();
        Task<Client> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(Client client);
    }
}
