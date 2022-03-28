using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ServicesRequest
    {
        [Key]
        public int ServiceRequestId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceStartDate { get; set; }
        [Required]
        public int Zipcode { get; set; }
        public int ServiceFrequency { get; set; }
        public decimal ServiceHourlyrate { get; set; }
        [Required]
        [MaxLength(5)]
        public float ServicesHours { get; set; }
        public float ExtraHours { get; set; }
        [Required]
        [MaxLength(5)]
        public decimal SubTotal { get; set; }
        [MaxLength(5)]
        public decimal Discount { get; set; }
        [Required]
        [MaxLength(5)]
        public decimal TotalCost { get; set; }
        [MaxLength(500)]
        public string Comments { get; set; }
        [MaxLength(50)]
        public string PaymentTransactionrefNo { get; set; }
        [Required]
        public int PaymentDue { get; set; }
        public bool JobStatus { get; set; }
        public string ServicesProviderId { get; set; }
        public DateTime SPAcceptedDate { get; set; }
        [Required]
        public bool HasPets { get; set; }
        public int Status { get; set; }
        [Required]
        public DateTime CraetedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        [MaxLength(5)]
        public decimal RefundedAmount { get; set; }
        public int Haslssue { get; set; }
        public bool PaymentDone { get; set; }
        public int uniqueidentifier { get; set; }
    }
}
