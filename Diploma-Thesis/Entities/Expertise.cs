using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma_Thesis.Entities
{
    public class Expertise
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public bool Result { get; set; }
        public string Notes { get; set; }


        [ForeignKey("ClientId")]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
