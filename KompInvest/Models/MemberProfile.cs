using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KompInvest.Models
{
    public class MemberProfile
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Occupation { get; set; }

        [StringLength(200)]
        public string InvestmentPreferences { get; set; }

        [Required]
        public DateTime MemberSince { get; set; }

        // Navigation Properties
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Investment> Investments { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
