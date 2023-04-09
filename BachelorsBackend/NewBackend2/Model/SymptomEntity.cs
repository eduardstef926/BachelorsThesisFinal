using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Symptom")]
    public class SymptomEntity
    {
        [Key]
        public string Symptom { get; set; }

        public virtual ICollection<UserSymptomMapping> UserSymptoms { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is SymptomEntity entity &&
                Symptom == entity.Symptom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Symptom);
        }
    }
}
