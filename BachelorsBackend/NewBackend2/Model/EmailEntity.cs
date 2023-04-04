using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Emails")]
    public class EmailEntity
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Message { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string To { get; set; }  

        [Required]
        public bool Send { get; set; }
    }
}
