using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace KompInvest.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
       
        public virtual ICollection<Comment> Comments { get; set; }

    }

}
