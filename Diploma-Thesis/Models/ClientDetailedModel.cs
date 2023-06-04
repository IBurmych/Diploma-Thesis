using Diploma_Thesis.Entities;

namespace Diploma_Thesis.Models
{
    public class ClientDetailedModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
