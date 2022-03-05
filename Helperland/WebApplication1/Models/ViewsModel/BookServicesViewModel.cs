using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class BookServicesViewModel
    {
        public string UserId { get; set; }
/*        [DataType(DataType.Date)]*/
        public DateTime ServicesStartDate { get; set; }
/*        [DataType(DataType.Time)]*/
        public DateTime StartTime { get; set; }
        public int zipCode { get; set; }
        public int AddressId { get; set; }
        public decimal TotalServices { get; set; }
        public string ServicesHours { get; set; }
        public decimal TotalCost { get; set; }
        public string Comments { get; set; }
        public bool HasPets { get; set; }
        public bool InsideCabinet { get; set; }
        public bool InsideFridge { get; set; }
        public bool InterioOven { get; set; }
        public bool LaundryWashDry { get; set; }
        public bool InteriorWindows { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
