using AutoMapper;
using Diploma_Thesis.Entities;
using Diploma_Thesis.Models;
using Diploma_Thesis.Repositories;

namespace Diploma_Thesis.Services
{
    public class ExpertisesService : IExpertisesService
    {
        private readonly IExpertisesRepository _expertisesRepository;
        private readonly IMapper _mapper;

        public ExpertisesService(IExpertisesRepository expertisesRepository, IMapper mapper)
        {
            _expertisesRepository = expertisesRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ExpertiseModel model)
        {
            if (model == null)
                return 0;

            var expertise = _mapper.Map<Expertise>(model);

            return await _expertisesRepository.AddAsync(expertise);
        }

        public IEnumerable<ExpertiseModel> GetByClientId(Guid clientId)
        {
            var expertises = _expertisesRepository.GetByClientId(clientId);

            if (expertises == null || !expertises.Any())
                return Enumerable.Empty<ExpertiseModel>();

            return _mapper.Map<IEnumerable<Expertise>, IEnumerable<ExpertiseModel>>(expertises);
        }
    }
}
