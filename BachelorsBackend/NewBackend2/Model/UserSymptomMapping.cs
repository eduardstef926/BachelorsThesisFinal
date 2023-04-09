using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("UserSymptomMapping")]
    public class UserSymptomMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserSymptomMappingId { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Symptom")]
        public string SymptomName { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual SymptomEntity Symptom { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UserSymptomMapping entity &&
                SymptomName == entity.SymptomName && 
                UserId == entity.UserId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, SymptomName);
        }
    }
}
