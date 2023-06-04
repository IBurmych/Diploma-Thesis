
using AutoMapper;
using Diploma_Thesis.Entities;
using Diploma_Thesis.Models;
using Diploma_Thesis.Repositories;

namespace Diploma_Thesis.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsService(IClientsRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public IEnumerable<ClientSimpleModel> GetAllClients()
        {
            var clients = _clientRepository.GetAll();

            if (clients == null || !clients.Any())
                return Enumerable.Empty<ClientSimpleModel>();

            return _mapper.Map<IEnumerable<Client>, IEnumerable<ClientSimpleModel>>(clients);
        }
        public async Task<ClientDetailedModel> GetClientDetailedAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                return null;

            return _mapper.Map<ClientDetailedModel>(client);
        }
        public async Task<int> AddClientAsync(ClientDetailedModel model)
        {
            if (model == null)
                return 0;

            model.Id = Guid.NewGuid();
            var client = _mapper.Map<Client>(model);
             
            return await _clientRepository.AddAsync(client);
        }

        public async Task<int> UpdateAsync(ClientDetailedModel model)
        {
            if (model == null)
                return 0;

            var client = _mapper.Map<Client>(model);

            return await _clientRepository.UpdateAsync(client);
        }
    }
}
