using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class RatingsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;

        public RatingsRepository(ApplicationDbContext db, IUserServicesHelper userServicesHelper, SendEmail sendEmail)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
        }
        public decimal? getRating(string servicesProviderId = "")
        {
            if (String.IsNullOrEmpty(servicesProviderId))
            {
                return null;
            }
            else
            {
                List<ServicesRating> servicesRatings = _db.ServicesRatings.Where(x => x.RatingTo == servicesProviderId).ToList();
                int count = 0;
                decimal sum = 0;
                foreach (var item in servicesRatings)
                {
                    sum += item.Ratings;
                    count++;
                }
                if (count == 0)
                {
                    return 0;
                }
                else
                {
                    return (sum / count);
                }
            }
        }
        /*Add Rating*/
        public bool Addrating(int ServicesRequestId, decimal onTime, decimal friendly, decimal qualityOfService, string feedBack)
        {
            ServicesRequest sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == ServicesRequestId).FirstOrDefault();
            ServicesRating rating = new ServicesRating();
            rating.ServicesrequestId = ServicesRequestId;
            rating.RatingFrom = sr.UserId;
            rating.RatingTo = sr.ServicesProviderId;
            rating.Comments = feedBack;
            rating.OnTimeArrival = onTime;
            rating.Friendly = friendly;
            rating.QualityOfServices = qualityOfService;
            decimal i = (friendly + qualityOfService + onTime) / 3;
            rating.Ratings = i;
            rating.RatingDate = DateTime.Now.ToShortDateString();
            _db.ServicesRatings.Add(rating);
            _db.SaveChanges();
            return true;
        }
    }
}
