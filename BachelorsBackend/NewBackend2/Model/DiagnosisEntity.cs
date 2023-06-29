using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Diagnosis")]
    public class DiagnosisEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiagnosticId { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        [ForeignKey("Disease")]
        public string? DiseaseName { get; set; } 

        public string SymptomList { get; set; }

        [Required]
        [MaxLength(100)]
        public string DoctorTitle { get; set; }

        [Required]
        [MaxLength(100)]
        public string DoctorSpecialization { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual DiseaseEntity Disease { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DiagnosisEntity entity &&
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
