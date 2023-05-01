using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Hospital")]
    public class HospitalEntity
    {
        [Key]
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<EmploymentEntity> Employments { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is HospitalEntity entity &&
                Name == entity.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
