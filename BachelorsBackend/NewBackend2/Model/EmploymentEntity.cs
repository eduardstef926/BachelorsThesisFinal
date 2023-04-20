using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Employment")]
    public class EmploymentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmploymentId { get; set; }

        [Required]
        [ForeignKey("Hospital")]
        public string HospitalName { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CurrentPosition { get; set; }

        [Required]
        public string ConsultPrice { get; set; }   

        [Required]
        [MaxLength(100)]
        public DayOfWeek WeekDay { get; set; }

        [Required]
        [MaxLength(100)]
        public TimeSpan StartTime { get; set; } 

        [Required]
        [MaxLength(100)]
        public TimeSpan EndTime { get; set; }

        public virtual DoctorEntity Doctor { get; set; }

        public virtual HospitalEntity Hospital { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is EmploymentEntity entity &&
                EmploymentId == entity.EmploymentId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EmploymentId);
        }
    }
}
