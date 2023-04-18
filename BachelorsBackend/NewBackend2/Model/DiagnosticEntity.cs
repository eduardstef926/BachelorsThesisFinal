using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Diagnostic")]
    public class DiagnosticEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiagnosticId { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Disease")]
        public string DiseaseName { get; set; } 

        [Required]
        [ForeignKey("Symptom")]
        public string SymptomList { get; set; }

        [Required]
        [MaxLength(100)]
        public string DoctorTitle { get; set; }

        [Required]
        [MaxLength(100)]
        public string DoctorSpecialization { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual DiseaseEntity Disease { get; set; }
        public virtual ICollection<SymptomEntity> Symptom { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DiagnosticEntity entity &&
                Disease == entity.Disease &&
                SymptomList == entity.SymptomList && 
                UserId == entity.UserId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, SymptomList, Disease);
        }
    }
}
