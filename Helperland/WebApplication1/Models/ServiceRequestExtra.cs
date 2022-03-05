using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ServiceRequestExtra
    {
        [Key]
        public int ServiceRequestExtraId { get; set; }
        public int ServicesRequestId { get; set; }
        public int ServicesExtraId { get; set; }
    }
}
