namespace Diploma_Thesis.Models
{
    public class ExpertiseModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool Result { get; set; }
        public string Notes { get; set; }

        public Guid ClientId { get; set; }
        public bool ExpectedResult { get; set; }
    }
}
