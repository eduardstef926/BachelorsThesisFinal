using NewBackend2.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Degree")]
    public class DegreeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DegreeId { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        [MaxLength(10)]
        public int DoctorId { get; set; }

        [Required]
        [ForeignKey("College")]
        [MaxLength(10)]
        public int CollegeId { get; set; }

        [Required]
        public DateTime StartYear { get; set; }

        [Required]
        public DateTime EndYear { get; set; }

        [Required]
        public StudyField StudyField { get; set; }

        [Required]
        public StudyProgram StudyProgram { get; set; }

        public DoctorEntity Doctor { get; set; }
        public CollegeEntity College { get; set; }
    }
}
