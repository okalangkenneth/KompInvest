using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class Testimonial
    {
        [Key]
        public int TestimonialID { get; set; }

        [ForeignKey("MemberProfile")]
        public int MemberID { get; set; }

        [Required, StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        // Navigation Property
        public virtual MemberProfile MemberProfile { get; set; }
    }
}
