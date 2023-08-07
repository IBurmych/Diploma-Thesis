using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma_Thesis.Entities
{
    public class Vector
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? InternalData { get; set; }
        public string? Comment { get; set; }
        [NotMapped]
        public double[] Data
        {
            get
            {
                return Array.ConvertAll(InternalData?.Split(';') ?? Array.Empty<string>(), Double.Parse);
            }
            set
            {
                InternalData = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
