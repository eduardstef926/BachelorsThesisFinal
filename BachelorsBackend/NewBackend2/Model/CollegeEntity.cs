using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("College")]
    public class CollegeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollegeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public virtual ICollection<DegreeEntity> DoctorColleges { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CollegeEntity entity &&
                Name == entity.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
