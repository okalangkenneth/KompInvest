using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class ForumPost
    {
        [Key]
        public int ForumPostId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        [Required]
        public string AuthorUserId { get; set; }  

        public virtual User Author { get; set; }  
    }

}
