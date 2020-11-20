using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DotNet5WebApi.Models
{

    [Table("transactions")]
    public class Transaction : ModelBase
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("transaction_date")]
        [Required]
        public DateTime TransactionDate { get; set; }

        [Column("code")]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Column("currency_code")]
        [Required]
        [StringLength(20)]
        public string CurrencyCode { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [NotMapped]
        string Payment {
            get => String.Format("{0:#,##0.00} {1}", Amount, CurrencyCode);
        }
        
        [Column("status")]
        [Required]
        public string Status { get; set; }
    }
}