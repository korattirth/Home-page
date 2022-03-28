using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class ServicesHistoryViewModel
    {
        public int ServiceId { get; set; }
        public string ServicesDate { get; set; }
        public string? ServicesProviderId { get; set; }
        public string ServicesProvideName { get; set; }
        public decimal Payment { get; set; }
        public int status { get; set; }
        public bool Rate { get; set; }
        public decimal? Rating { get; set; }
    }
}
