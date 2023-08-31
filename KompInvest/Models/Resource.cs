using System.ComponentModel.DataAnnotations;

namespace KompInvest.Models
{
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        [Required]
        [StringLength(100)]
        public string ResourceName { get; set; }

        public string ResourceURL { get; set; }

        public byte[] ResourceFile { get; set; }  // If you want to store the file itself in the database

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }  
    }

}
