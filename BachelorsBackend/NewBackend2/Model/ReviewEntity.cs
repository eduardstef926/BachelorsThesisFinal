using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Review")]
    public class ReviewEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewMappingId { get; set; }

        [Required]
        [ForeignKey("AppointmentEntity")]
        [MaxLength(10)]
        public int AppointmentId { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [MaxLength(200)]
        public string Message { get; set; }

        public virtual AppointmentEntity Appointment { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReviewEntity entity &&
                AppointmentId == entity.AppointmentId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AppointmentId);
        }
    }
}
