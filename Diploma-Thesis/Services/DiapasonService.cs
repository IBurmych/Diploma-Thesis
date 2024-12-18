using AutoMapper;
using Diploma_Thesis.Entities;
using Diploma_Thesis.Utils;
using Diploma_Thesis.Models;
using Diploma_Thesis.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Diploma_Thesis.Services
{
    public class DiapasonService : IDiapasonService
    {
        public IDiapasonsRepository _diapasonsRepository;
        public IMapper _mapper;

        public DiapasonService(IDiapasonsRepository diapasonsRepository, IMapper mapper)
        {
            _diapasonsRepository = diapasonsRepository;
            _mapper = mapper;
        }
        public DiapasonСrossingModel GetСrossing(DiapasonModel firstDiapason, DiapasonModel secondDiapason)
        {
            if (firstDiapason == null || secondDiapason == null || firstDiapason.Element == secondDiapason.Element)
            {
                return null;
            }
            var crossing = new DiapasonСrossingModel();
            crossing.IsCrossing = false;

            DiapasonModel minDiapason = firstDiapason.Element < secondDiapason.Element ? firstDiapason : secondDiapason;
            DiapasonModel maxDiapason = firstDiapason.Element > secondDiapason.Element ? firstDiapason : secondDiapason;

            if(minDiapason.ElementMax > maxDiapason.ElementMin)
            {
                crossing.IsCrossing = true;
                crossing.Difference = minDiapason.ElementMax - maxDiapason.ElementMin;
            }

            crossing.Difference = maxDiapason.ElementMin - minDiapason.ElementMax;
            return crossing;
        }
        public async Task<IEnumerable<FullDiapasonModel>> SaveDiapasons(List<FullDiapasonModel> diapasons)
        {
            if (diapasons.IsNullOrEmpty())
            { 
                return Enumerable.Empty<FullDiapasonModel>();
            }

            foreach (var diapason in diapasons)
            {
                if (diapason == null) continue;

                List<decimal> nums = new List<decimal>() { diapason.ElementMax, diapason.ElementMin, diapason.ProblemElementMax, diapason.ProblemElementMin };
                decimal minNum = nums.Min();
                decimal maxNum = nums.Max();
                decimal delta = maxNum - minNum;
                decimal diapasonStep = delta / 8;

                if(diapason.ElementMin.IsBetween(minNum, minNum + diapasonStep))
                {
                    diapason.ElementGrades = DiapasonGradeEnum.H;
                }
                if (diapason.ProblemElementMin.IsBetween(minNum, minNum + diapasonStep))
                {
                    diapason.ProblemElementGrades = DiapasonGradeEnum.H;
                }

                for (byte i = 0; i < 4; i++)
                {
                    if (diapason.ElementMax.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep) ||
                        diapason.ElementMin.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep))
                    {
                        diapason.ElementGrades += (byte)Math.Pow(2, (i + 1));
                    }

                    if (diapason.ProblemElementMax.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep) || 
                        diapason.ProblemElementMin.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep))
                    {
                        diapason.ProblemElementGrades += (byte)Math.Pow(2, (i + 1));
                    }
                }

                if (diapason.ElementMax.IsBetween(maxNum - diapasonStep, maxNum))
                {
                    diapason.ElementGrades |= DiapasonGradeEnum.B;
                }
                if (diapason.ProblemElementMax.IsBetween(maxNum - diapasonStep, maxNum))
                {
                    diapason.ProblemElementGrades |= DiapasonGradeEnum.B;
                }
            }
            
            var entities = _mapper.Map<IEnumerable<FullDiapasonModel>, IEnumerable<Diapason>>(diapasons);
            int savedCount = await _diapasonsRepository.AddRangeAsync(entities);
            if (savedCount > 0) return diapasons;

            return Enumerable.Empty<FullDiapasonModel>();
        }
        public async Task<string> ChooseThin(Dictionary<string, decimal> values)
        {
            var diapasons = await _diapasonsRepository.GetAllAsync();

            diapasons = diapasons.Where(x => x.CalcName == "2").ToList();

            Dictionary<string, DiapasonGradeEnum> valuesDiapasons = new Dictionary<string, DiapasonGradeEnum>();
            Dictionary<string, DiapasonGradeEnum> valuesProblemDiapasons = new Dictionary<string, DiapasonGradeEnum>();

            foreach (var diapason in diapasons)
            {
                values.TryGetValue(diapason.Name, out decimal value);


                if (diapason == null) continue;

                List<decimal> nums = new List<decimal>() { diapason.ElementMax, diapason.ElementMin, diapason.ProblemElementMax, diapason.ProblemElementMin };
                decimal minNum = nums.Min();
                decimal maxNum = nums.Max();
                decimal delta = maxNum - minNum;
                decimal diapasonStep = delta / 8;

                if (diapason.ElementMin.IsBetween(minNum, minNum + diapasonStep) && value.IsBetween(minNum, minNum + diapasonStep))
                {
                    valuesDiapasons.Add(diapason.Name, DiapasonGradeEnum.H);
                    continue;
                }
                if (diapason.ProblemElementMin.IsBetween(minNum, minNum + diapasonStep) && value.IsBetween(minNum, minNum + diapasonStep))
                {
                    valuesProblemDiapasons.Add(diapason.Name, DiapasonGradeEnum.H);
                    continue;
                }

                for (byte i = 0; i < 4; i++)
                {
                    if ((diapason.ElementMax.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep) ||
                        diapason.ElementMin.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep)) &&
                        value.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1)))
                    {
                        valuesDiapasons.Add(diapason.Name, (DiapasonGradeEnum)(byte)Math.Pow(2, (i + 1)));
                        continue;
                    }

                    if (diapason.ProblemElementMax.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep) ||
                        diapason.ProblemElementMin.IsBetween(minNum + (i * 2 + 1) * diapasonStep, minNum + ((i + 1) * 2 + 1) * diapasonStep))
                    {
                        valuesProblemDiapasons.Add(diapason.Name, (DiapasonGradeEnum)(byte)Math.Pow(2, (i + 1)));
                        continue;
                    }
                }

                if (diapason.ElementMax.IsBetween(maxNum - diapasonStep, maxNum))
                {
                    valuesDiapasons.Add(diapason.Name, DiapasonGradeEnum.B);
                    continue;
                }
                if (diapason.ProblemElementMax.IsBetween(maxNum - diapasonStep, maxNum))
                {
                    valuesProblemDiapasons.Add(diapason.Name, DiapasonGradeEnum.B);
                    continue;
                }
            }

            bool healthy = false;

            if (!valuesDiapasons.IsNullOrEmpty())
            {
                valuesDiapasons.TryGetValue("Дисперсія D", out DiapasonGradeEnum d);
                valuesDiapasons.TryGetValue("Асиметрія A", out DiapasonGradeEnum a);
                valuesDiapasons.TryGetValue("Ексцес E", out DiapasonGradeEnum e);
                valuesDiapasons.TryGetValue("Другий кореляційний момент S", out DiapasonGradeEnum s);
                valuesDiapasons.TryGetValue("Перший кореляційний момент L", out DiapasonGradeEnum l);

                healthy =
                (d == DiapasonGradeEnum.BC && a == DiapasonGradeEnum.BC && e == DiapasonGradeEnum.H && l == DiapasonGradeEnum.H && s == DiapasonGradeEnum.BC) ||
                (d == DiapasonGradeEnum.BC && a == DiapasonGradeEnum.BC && e == DiapasonGradeEnum.HC && l == DiapasonGradeEnum.H && s == DiapasonGradeEnum.BC) ||
                (d == DiapasonGradeEnum.BC && a == DiapasonGradeEnum.B && e == DiapasonGradeEnum.H && l == DiapasonGradeEnum.H && s == DiapasonGradeEnum.B) ||
                (d == DiapasonGradeEnum.BC && a == DiapasonGradeEnum.B && e == DiapasonGradeEnum.HC && l == DiapasonGradeEnum.H && s == DiapasonGradeEnum.B) ||
                (d == DiapasonGradeEnum.B && a == DiapasonGradeEnum.BC && e == DiapasonGradeEnum.H && l == DiapasonGradeEnum.H && s == DiapasonGradeEnum.BC) ||
                (d == DiapasonGradeEnum.B && a == DiapasonGradeEnum.B && e == DiapasonGradeEnum.HC && l == DiapasonGradeEnum.H && s == DiapasonGradeEnum.B);
            }
            string healthyStr = healthy ? "здоровий" : "";

            bool unwell = false;
            if (!valuesDiapasons.IsNullOrEmpty())
            {
                valuesProblemDiapasons.TryGetValue("Дисперсія D", out DiapasonGradeEnum d);
                valuesProblemDiapasons.TryGetValue("Асиметрія A", out DiapasonGradeEnum a);
                valuesProblemDiapasons.TryGetValue("Ексцес E", out DiapasonGradeEnum e);
                valuesProblemDiapasons.TryGetValue("Другий кореляційний момент S", out DiapasonGradeEnum s);
                valuesProblemDiapasons.TryGetValue("Перший кореляційний момент L", out DiapasonGradeEnum l);

                unwell =
                (d == DiapasonGradeEnum.H && a == DiapasonGradeEnum.H && e == DiapasonGradeEnum.BC && l == DiapasonGradeEnum.BC && s == DiapasonGradeEnum.H) ||
                (d == DiapasonGradeEnum.H && a == DiapasonGradeEnum.HC && e == DiapasonGradeEnum.BC && l == DiapasonGradeEnum.B && s == DiapasonGradeEnum.HC) ||
                (d == DiapasonGradeEnum.HC && a == DiapasonGradeEnum.H && e == DiapasonGradeEnum.B && l == DiapasonGradeEnum.BC && s == DiapasonGradeEnum.H) ||
                (d == DiapasonGradeEnum.HC && a == DiapasonGradeEnum.HC && e == DiapasonGradeEnum.B && l == DiapasonGradeEnum.B && s == DiapasonGradeEnum.HC);
            }

            string unwellStr = unwell ? "сепсис тонкої кишки" : "";

            return string.Join(", ", healthyStr, unwellStr);
        }
        public async Task<IEnumerable<FullDiapasonModel>> GetAllAsync()
        {
            var diapasonsEntities = await _diapasonsRepository.GetAllAsync();
            var diapasons = _mapper.Map<IEnumerable<Diapason>, IEnumerable<FullDiapasonModel>>(diapasonsEntities);
            return diapasons;
        }
    }
}
