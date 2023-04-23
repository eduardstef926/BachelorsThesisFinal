using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Subscription")]
    public class SubscriptionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubscriptionId { get; set; }

        [Required]
        [ForeignKey("User")]
        [MaxLength(10)]
        public int UserId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public UserEntity User { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is SubscriptionEntity entity &&
                SubscriptionId == entity.SubscriptionId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SubscriptionId);
        }
    }
}
