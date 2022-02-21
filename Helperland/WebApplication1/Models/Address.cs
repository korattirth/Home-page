using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public Double PhoneNumber { get; set; }
        public int PostalCode { get; set; }
    }
}
