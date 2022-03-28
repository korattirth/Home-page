using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ServicesRating
    {
        [Key]
        public int RatingId { get; set; }
        public int ServicesrequestId { get; set; }
        public string RatingFrom { get; set; }
        public string RatingTo { get; set; }
        public decimal Ratings { get; set; }
        public string Comments { get; set; }
        public string RatingDate { get; set; }
        public bool IsApproved { get; set; }
        public bool VisibleOnHomeScreen { get; set; }
        public decimal OnTimeArrival { get; set; }
        public decimal Friendly { get; set; }
        public decimal QualityOfServices { get; set; }
    }
}
