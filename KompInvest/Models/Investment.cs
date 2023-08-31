using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class Investment
    {
        [Key]
        public int InvestmentId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvestmentName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime InvestmentDate { get; set; }

        public virtual User User { get; set; }  
    }

}
