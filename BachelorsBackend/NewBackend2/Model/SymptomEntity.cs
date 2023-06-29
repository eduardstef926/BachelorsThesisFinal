using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Symptom")]
    public class SymptomEntity
    {
        [Key]
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is SymptomEntity entity &&
                Name == entity.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
