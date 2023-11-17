using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KompInvest.Models
{
    public class Investment
    {
        [Key]
        public int InvestmentID { get; set; }

        [ForeignKey("MemberProfile")]
        public int MemberID { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal AmountInvested { get; set; }

        [Required]
        public DateTime DateInvested { get; set; }

        [Required, DataType(DataType.Currency)]
        

        [StringLength(1000)] // Assuming JSON or URL
        public string PerformanceData { get; set; }

        // Navigation Properties
        public virtual MemberProfile MemberProfile { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }

}
