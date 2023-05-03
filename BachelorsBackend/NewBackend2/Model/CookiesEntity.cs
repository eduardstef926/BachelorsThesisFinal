using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Cookies")]
    public class CookiesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CookieId { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public DateTime DateTime { get; set; }

        public virtual UserEntity User { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CookiesEntity entity &&
                CookieId == entity.CookieId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CookieId);
        }
    }
}
