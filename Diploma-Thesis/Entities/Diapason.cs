using Diploma_Thesis.Utils;

namespace Diploma_Thesis.Entities
{
    public class Diapason
    {
        public Guid Id { get; set; }
        public string? CalcName { get; set; }
        public string? Name { get; set; }
        public decimal Element { get; set; }
        public decimal ElementRange { get; set; }
        public decimal ElementMax { get { return Element + ElementRange; } }
        public decimal ElementMin { get { return Element - ElementRange; } }
        public DiapasonGradeEnum ElementGrades { get; set; }
        public decimal ProblemElement { get; set; }
        public decimal ProblemElementRange { get; set; }
        public decimal ProblemElementMax { get { return ProblemElement + ProblemElementRange; } }
        public decimal ProblemElementMin { get { return ProblemElement - ProblemElementRange; } }
        public DiapasonGradeEnum ProblemElementGrades { get; set; }
        public bool IsCrossing { get; set; }
        public decimal Difference { get; set; }
    }
}
