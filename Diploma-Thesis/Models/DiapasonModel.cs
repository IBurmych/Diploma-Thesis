namespace Diploma_Thesis.Models
{
    public class DiapasonModel
    {
        public decimal Element { get; set; }
        public decimal ElementRange { get; set; }
        public decimal ElementMax { get { return Element + ElementRange; } }
        public decimal ElementMin { get { return Element - ElementRange; } }
    }
}
