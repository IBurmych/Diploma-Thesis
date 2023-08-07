using AutoMapper;
using Diploma_Thesis.Entities;
using Diploma_Thesis.Models;
using Diploma_Thesis.Repositories;

namespace Diploma_Thesis.Services
{
    public class ExpertisesService : IExpertisesService
    {
        private readonly IExpertisesRepository _expertisesRepository;
        private readonly IVectorsRepository _vectorsRepository;
        private readonly IMapper _mapper;

        private readonly double[] normal = new double[24] { 0.32, 0.08, 2.31, 1.29, 0.42, 0.11, 1.38, 4.55, 0.57, 0.34, 0.13, 0.19, 0.34, 0.13, 0.52, 0.45, 0.46, 0.25, 0.37, 0.43, 0.73, 0.3, 0.11, 0.14 };
        private readonly double[] normalMistake = new double[24] { 0.022, 0.005, 0.18, 0.011, 0.039, 0.01, 0.12, 0.32, 0.05, 0.023, 0.01, 0.014, 0.029, 0.011, 0.047, 0.038, 0.04, 0.019, 0.025, 0.039, 0.057, 0.028, 0.01, 0.01 };
        private readonly double[] problem = new double[24] { 0.28, 0.19, 0.75, 3.78, 0.38, 0.22, 0.29, 0.73, 0.61, 0.25, 0.36, 0.48, 0.23, 0.06, 1.76, 2.30, 0.40, 0.21, 0.42, 1.49, 0.64, 0.38, 0.17, 0.22 };
        private readonly double[] problemMistake = new double[24] { 0.021, 0.015, 0.055, 0.22, 0.035, 0.018, 0.19, 0.055, 0.045, 0.02, 0.025, 0.039, 0.02, 0.004, 0.14, 0.18, 0.028, 0.016, 0.036, 0.13, 0.058, 0.027, 0.012, 0.016 };

        public ExpertisesService(IExpertisesRepository expertisesRepository, IVectorsRepository vectorsRepository, IMapper mapper)
        {
            _expertisesRepository = expertisesRepository;
            _vectorsRepository = vectorsRepository;
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

        public async Task<int> UpdateAsync(ExpertiseModel model)
        {
            if (model == null)
                return 0;

            var expertises = _mapper.Map<Expertise>(model);

            return await _expertisesRepository.UpdateAsync(expertises);
        }

        public async Task<ExpertiseModel> AnalyzePhoto(IFormFile photo, Guid clientId)
        {
            bool res = AnalyzeVector(GetRandomVector().Data);
            var expertise = new ExpertiseModel() { Date = DateTime.Now, Id = Guid.NewGuid(), Notes = "", Result = res, ClientId = clientId };
            await AddAsync(expertise);
            return expertise;
        }

        public IEnumerable<ExpertiseModel> AnalyseAllVectors()
        {
            var vectors = _vectorsRepository.GetAllVectors();
            return vectors.Select(x => new ExpertiseModel()
                {
                    ClientId = Guid.Empty,
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Notes = "",
                    Result = AnalyzeVector(x.Data),
                    ExpectedResult = x.Comment == "normal"
            }
            );;
        }
        public Vector GetRandomVector()
        {
            var vectors = _vectorsRepository.GetAllVectors();

            var rnd = new Random();
            var rndIndex = rnd.Next(vectors.Count());

            return vectors.ElementAt(rndIndex);
        }

        public async Task<int> GenerateTestVectors()
        {
            List<Vector> vectors = new List<Vector>();

            for (int i = 0; i < 21; i++)
            {
                vectors.Add(new Vector()
                {
                    Data = AddRandomMistake(normal, normalMistake).ToArray(),
                    Comment = "normal"
                });
                vectors.Add(new Vector()
                {
                    Data = AddRandomMistake(problem, problemMistake).ToArray(),
                    Comment = "problem"
                });
            }

            return await _vectorsRepository.AddRange(vectors);
        }
        private List<double> AddRandomMistake(double[] vector, double[] mistake)
        {
            List<double> newVector = new List<double>();
            for (int i = 0; i < vector.Length; i++)
            {
                var rnd = new Random();
                double rndMistake = rnd.Next((int)(mistake[i] * 1000 + 1)) * 0.001d;
                int rndSign = rnd.Next(2);

                if(rndSign == 1)
                    rndMistake *= -1;

                newVector.Add(vector[i] + rndMistake);
            }
            return newVector;
        }

        private bool AnalyzeVector(double[] vector)
        {
            var normalDistance = GetDistance(normal, vector);
            var problemDistance = GetDistance(problem, vector);
            return normalDistance < problemDistance;
        }

        private double GetDistance(double[] firstVector, double[] secondVector)
        {
            double sum = 0;
            for(int i = 0; i < firstVector.Length; i++)
            {
                sum += Math.Pow(firstVector[i] - secondVector[i], 2);
            }
            return Math.Sqrt(sum);
        }
    }
}
