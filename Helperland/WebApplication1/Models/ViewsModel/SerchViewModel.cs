using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class SerchViewModel
    {
        public int ServicerequestId { get; set; }
        public int PostalCode { get; set; }
        public string Email { get; set; }
        public string Customer { get; set; }
        public string ServiceProvider { get; set; }
        public int Status { get; set; }
        public string FromDate { get; set; }
        public bool HasIssue { get; set; }
        public string Todate { get; set; }

    }
}
