using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Disease")]
    public class DiseaseEntity
    {
        [Key]
        public string Name { get; set; }

        public virtual ICollection<DiagnosisEntity> Diagnosis { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DiseaseEntity entity &&
                Name == entity.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
