using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesData.Models
{
    public class SalesModel
    {
        [Key]
        public int Id { get; set; }
        public int BranchId { get; set; }
        [Required, StringLength(30)]
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        [DataType("decimal(16 ,3)")]
        public float Amount { get; set; }
        [StringLength(30)]
        public string? LoyaltyCardNumber { get; set; }
    }
}
