using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Appointment")]
    public class AppointmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        [MaxLength(10)]
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(100)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        [MaxLength(100)]
        public string HospitalName { get; set; }

        [Required]
        public int Price { get; set; }  

        public virtual UserEntity User { get; set; }

        public virtual DoctorEntity Doctor { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is AppointmentEntity entity &&
                UserId == entity.UserId &&
                DoctorId == entity.DoctorId;    
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, DoctorId);
        }
    }
}
