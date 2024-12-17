using Diploma_Thesis.Utils;

namespace Diploma_Thesis.Models
{
    public class FullDiapasonModel
    {
        public string? CalcName { get; set; }
        public string? Name { get; set; }
        public decimal Element { get; set; }
        public decimal ElementRange { get; set; }
        public decimal ElementMax { get { return Element + ElementRange; } }
        public decimal ElementMin { get { return Element - ElementRange; } }
        public DiapasonGradeEnum ElementGrades { get; set; } = 0;
        public string ElementGradesString { get { return ElementGrades.ToString(); } }
        public decimal ProblemElement { get; set; }
        public decimal ProblemElementRange { get; set; }
        public decimal ProblemElementMax { get { return ProblemElement + ProblemElementRange; } }
        public decimal ProblemElementMin { get { return ProblemElement - ProblemElementRange; } }
        public DiapasonGradeEnum ProblemElementGrades { get; set; } = 0;
        public string ProblemElementGradesString { get { return ProblemElementGrades.ToString(); } }
        public bool? IsCrossing { get; set; }
        public decimal Difference { get; set; }
    }
}
