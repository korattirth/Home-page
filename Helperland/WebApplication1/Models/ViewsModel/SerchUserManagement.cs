using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class SerchUserManagement
    {
        public string UserName { get; set; }
        public decimal PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string Email { get; set; }
        public int Usertype { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
