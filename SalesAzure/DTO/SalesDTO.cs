using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAzure.DTO
{
    public class SalesDTO
    {
        [Key]
        public int Id { get; set; }
        public int BranchId { get; set; }
        [Required]
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public float Amount { get; set; }
        public string LoyaltyCardNumber { get; set; }
    }
}
