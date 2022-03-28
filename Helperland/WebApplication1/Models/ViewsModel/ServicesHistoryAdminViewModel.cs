using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class ServicesHistoryAdminViewModel
    {
        public IEnumerable<ServiceRequestAdminViewModel> serviceRequestAdmin { get; set; }
        public SerchViewModel SerchHistory { get; set; }
    }
}
