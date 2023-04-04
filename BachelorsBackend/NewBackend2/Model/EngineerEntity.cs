using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Engineer")]
    public class EngineerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EngineerId { get; set; }

        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Password { get; set; }

        [Required]
        [MaxLength(10)]
        public int PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public int Experience { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is EngineerEntity entity &&
                FirstName == entity.FirstName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName);
        }
    }
}
