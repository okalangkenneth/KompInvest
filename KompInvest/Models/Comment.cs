using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        [Required]
        public int BlogId { get; set; }  

        [Required]
        public string CommenterUserId { get; set; }  

        public virtual Blog Blog { get; set; }  
        public virtual User Commenter { get; set; }  
    }

}
