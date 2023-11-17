using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        public bool IsMembersOnly { get; set; }
    }

}
