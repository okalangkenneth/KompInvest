using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class UserProfile
    {
        [Key]
        public int ProfileId { get; set; }

        [Required]
        public string UserId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string Bio { get; set; }

        public virtual User User { get; set; }  
    }

}
