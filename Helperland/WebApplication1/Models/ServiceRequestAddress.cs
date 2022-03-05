using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ServiceRequestAddress
    {
        [Key]
        public int Id { get; set; }
        public int ServicesRequestId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string city { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public double PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int Type { get; set; }
    }
}
