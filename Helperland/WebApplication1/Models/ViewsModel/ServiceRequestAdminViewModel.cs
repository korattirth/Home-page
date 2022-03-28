using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class ServiceRequestAdminViewModel
    {
        public int ServicesRequestId { get; set; }
        public string ServiceStartDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public int Postalcode { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public decimal Rating { get; set; }
        public string Avtar { get; set; }
        public decimal NetAmount { get; set; }
        public int Status { get; set; }
    }
}
