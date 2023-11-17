using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class NewsArticle
    {
        [Key]
        public int ArticleID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(2000)]
        public string Content { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [StringLength(200)]
        public string Source { get; set; }

        [Required]
        public bool IsInternational { get; set; }
    }
}
