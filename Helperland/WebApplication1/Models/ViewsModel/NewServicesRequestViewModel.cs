using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class NewServicesRequestViewModel
    {
        public int ServicesRequestId { get; set; }
        public string ServicesStartDate { get; set; }
        public int MyProperty { get; set; }
        public decimal Payment { get; set; }
        public int Status { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public int Postalcode { get; set; }
        public string FIrstName { get; set; }
        public string LastName { get; set; }

    }
}
