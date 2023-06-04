using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Repositories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly Context _db;

        public ClientsRepository(Context db)
        {
            _db = db;
        }
        public async Task<int> AddAsync(Client client)
        {
            await _db.Clients.AddAsync(client);
            return await _db.SaveChangesAsync();
        }

        public IEnumerable<Client> GetAll()
        {
            return _db.Clients;
        }
        public async Task<Client> GetByIdAsync(Guid id)
        {
            return await _db.Clients.FindAsync(id);
        }
        public async Task<int> UpdateAsync(Client client)
        {
            _db.Clients.Update(client);
            return await _db.SaveChangesAsync();
        }
    }
}
