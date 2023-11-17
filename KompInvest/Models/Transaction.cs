using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace KompInvest.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [ForeignKey("Investment")]
        public int InvestmentID { get; set; }

        [ForeignKey("MemberProfile")]
        public int MemberID { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; }

        [Required, DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        // Navigation Properties
        public virtual Investment Investment { get; set; }
        public virtual MemberProfile MemberProfile { get; set; }
    }
}