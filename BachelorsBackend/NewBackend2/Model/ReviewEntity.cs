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
        [ForeignKey("Doctor")]
        [MaxLength(10)]
        public int DoctorId { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        [MaxLength(200)]
        public string Message { get; set; }

        public virtual UserEntity User { get; set; }

        public virtual DoctorEntity Doctor { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ReviewEntity entity &&
                UserId == entity.UserId &&
                DoctorId == entity.DoctorId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, DoctorId);
        }
    }
}
