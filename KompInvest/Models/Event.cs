using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        [Required]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }  
    }

}
