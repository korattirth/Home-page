using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class RatingViewModel
    {
        public int ServicesRequestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RatingDate { get; set; }
        public decimal Rating { get; set; }
        public string Comments { get; set; }
    }
}
