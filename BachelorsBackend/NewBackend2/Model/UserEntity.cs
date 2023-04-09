using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public int PhoneNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public bool isEmailConfirmed { get; set; }

        public virtual ICollection<UserSymptomMapping> UserSymptoms { get; set; }
        public virtual ICollection<AppointmentEntity> Appointments { get; set; }
        public virtual ICollection<ReviewEntity> Reviews { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is UserEntity entity &&
                FirstName == entity.FirstName &&
                LastName == entity.LastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
        }
    }
}
