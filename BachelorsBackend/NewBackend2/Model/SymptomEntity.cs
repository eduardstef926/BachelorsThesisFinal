using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Symptom")]
    public class SymptomEntity
    {
        [Key]
        public string Symptom { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }

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
