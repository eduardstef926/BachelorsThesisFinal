﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBackend2.Model
{
    [Table("Doctor")]
    public class DoctorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

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
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public int PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        public virtual AppointmentEntity Appointment { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DoctorEntity entity &&
                FirstName == entity.FirstName && 
                LastName == entity.LastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
        }
    }
}
