using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Models.ViewsModel;

namespace WebApplication1.Repository
{
    public class ServicesRequestRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;
        private readonly SendEmail _sendEmail;
        private readonly RatingsRepository _ratingsRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServicesRequestRepository(ApplicationDbContext db, IUserServicesHelper userServicesHelper ,SendEmail sendEmail ,RatingsRepository ratingsRepository, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
            _sendEmail = sendEmail;
            _ratingsRepository = ratingsRepository;
            _userManager = userManager;
        }
        public List<CurrentServicesViewModel> getDetailsDashboard()
        {
            var userid = _userServicesHelper.getUserId();
            List<ServicesRequest> sr = new List<ServicesRequest>();
            sr = _db.ServicesRequests.Where(x => x.UserId == userid && x.Status == 0).ToList();
            List<CurrentServicesViewModel> currentServices = new List<CurrentServicesViewModel>();
            foreach (var item in sr)
            {
                CurrentServicesViewModel l = new CurrentServicesViewModel();
                l.ServiceId = item.ServiceRequestId;
                l.ServicesDate = item.ServiceStartDate;
                l.Payment = item.TotalCost;
                l.ServicesProviderId = item.ServicesProviderId;
                string serviceProviderName = null;
                if (!String.IsNullOrEmpty(item.ServicesProviderId))
                {
                    var FirstName = _db.Users.Where(x => x.Id == l.ServicesProviderId).Select(x => new { firstName = x.FirstName }).Single();
                    serviceProviderName = FirstName.firstName;
                    var LastName = _db.Users.Where(x => x.Id == l.ServicesProviderId).Select(x => new { lastName = x.LastName }).Single();
                    serviceProviderName += LastName.lastName;
                }
                l.ServicesProvideName = serviceProviderName;
                l.status = item.Status;
                l.Rating = _ratingsRepository.getRating(item.ServicesProviderId);
                currentServices.Add(l);
            }
            return currentServices;
        }
        /*UpDate Services Date*/
        public bool UpdateServicesDate(int requestId, DateTime UpdateDate)
        {
            ServicesRequest servicesRequest = _db.ServicesRequests.Where(x => x.ServiceRequestId == requestId).FirstOrDefault();
            if(servicesRequest.ServicesProviderId == null)
            {
                servicesRequest.ServiceStartDate = UpdateDate.ToString("yyyy/MM/dd");
                _db.ServicesRequests.Update(servicesRequest);
                _db.SaveChanges();
                ApplicationUser user = _db.Users.Where(x => x.Id == servicesRequest.UserId).FirstOrDefault();
                string body = "Message From HelperLand <br/> Your Shchedule is Updated Please cheak once Your Shcedule";
                string To = user.Email;
                string subject = "Shcedule Updated";
                _sendEmail.SendMail(To, subject, body);
                return true;
            }
            else
            {
                List<ServicesRequest> sr = _db.ServicesRequests.Where(x => x.ServicesProviderId == servicesRequest.ServicesProviderId && x.ServiceStartDate == UpdateDate.ToShortDateString()).ToList();
                if (sr == null)
                {
                    servicesRequest.ServiceStartDate = UpdateDate.ToString("yyyy/MM/dd");
                    _db.ServicesRequests.Update(servicesRequest);
                    _db.SaveChanges();
                    ApplicationUser user = _db.Users.Where(x => x.Id == servicesRequest.UserId).FirstOrDefault();
                    string body = "Message From HelperLand <br/> Your Shchedule is Updated Please cheak once Your Shcedule";
                    string To = user.Email;
                    string subject = "Shcedule Updated";
                    _sendEmail.SendMail(To, subject, body);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /*Cancel Request*/
        public bool CancelRequest(int requestId ,string cancelReason)
        {
            ServicesRequest sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == requestId).FirstOrDefault();
            sr.Status = 2;
            _db.ServicesRequests.Update(sr);
            _db.SaveChanges();
            if(sr.ServicesProviderId != null)
            {
                string body = "Welcome To HelperLand <br/> Services RequestNo : " + sr.ServiceRequestId + " is Cancel Due To "+ cancelReason + " ";
                var userEmail = _db.Users.Where(x => x.Id == sr.ServicesProviderId).Select(x => new { email = x.Email }).Single();
                string To = userEmail.email;
                string subject = "Cancel Services Request";
                _sendEmail.SendMail(To, subject, body);
            }
            return true;
        }
        /*Get Services History*/
        public List<ServicesHistoryViewModel> GetServicesHistory()
        {
            var userid = _userServicesHelper.getUserId();
            List<ServicesRequest> sr = _db.ServicesRequests.Where(x => x.UserId == userid && (x.Status == 2 || x.Status == 1)).ToList();
            List<ServicesHistoryViewModel> shv = new List<ServicesHistoryViewModel>();
            foreach (var item in sr)
            {
                ServicesHistoryViewModel ob = new ServicesHistoryViewModel();
                ob.ServiceId = item.ServiceRequestId;
                ob.ServicesProviderId = item.ServicesProviderId;
                string serviceProviderName = null;
                if (!String.IsNullOrEmpty(ob.ServicesProviderId))
                {
                    var FirstName = _db.Users.Where(x => x.Id == ob.ServicesProviderId).Select(x => new { firstName = x.FirstName }).Single();
                    serviceProviderName = FirstName.firstName;
                    var LastName = _db.Users.Where(x => x.Id == ob.ServicesProviderId).Select(x => new { lastName = x.LastName }).Single();
                    serviceProviderName += LastName.lastName;
                }
                ob.ServicesProvideName = serviceProviderName;
                ob.ServicesDate = item.ServiceStartDate;
                ob.Payment = item.TotalCost;
                ob.status = item.Status;
                if(ob.status == 2)
                {
                    ob.Rate = false;
                }
                else if(String.IsNullOrEmpty(ob.ServicesProviderId)){
                    ob.Rate = false;
                }
                else
                {
                    ServicesRating servicesRating = _db.ServicesRatings.Where(x => x.ServicesrequestId == item.ServiceRequestId).FirstOrDefault();
                    if(servicesRating == null)
                    {
                        ob.Rate = true;
                    }
                    else
                    {
                        ob.Rate = false;
                    }
                }
                ob.Rating = _ratingsRepository.getRating(item.ServicesProviderId);
                shv.Add(ob);
            }
            return shv;
        }
        /*Update User Details*/
        public bool UpdateuserDetails(string firstName ,string lastName,string email,string phoneNumber,DateTime dateOfBirth ,int language ,string userId) 
        {
            ApplicationUser ap=_db.Users.Where(x => x.Id == userId).FirstOrDefault();
            ap.FirstName = firstName;
            ap.LastName = lastName;
            ap.Email = email;
            ap.PhoneNumber = phoneNumber;
            ap.UserName = email;
            ap.NormalizedEmail = email.ToUpper();
            ap.NormalizedUserName = email.ToUpper();
            ap.DateOfBirth = dateOfBirth.ToString("yyyy/MM/dd");
            ap.LanguageId = language;
            _db.Users.Update(ap);
            _db.SaveChanges();
            return true;
        }
       /*Add Address*/
       public bool addAddress(string streetName , string houseNumber ,string city , double phoneNumber , int postalCode, string userId)
        {
            UserAddress userAddress = new UserAddress()
            {
                UserId = userId,
                City = city,
                StreetName = streetName,
                HouseNumber = houseNumber,
                PhoneNumber = phoneNumber,
                PostalCode = postalCode,
            };
            ApplicationUser user =_db.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.Zipcode = postalCode;
            _db.Users.Update(user);
            _db.addresses.Add(userAddress);
            _db.SaveChanges();
            return true;
        }
        /*Update Address By Address Id*/
        public bool UpdateAddress(string streetName, string houseNumber, string city, double phoneNumber, int postalCode, int AddressId)
        {
            var userId = _userServicesHelper.getUserId();
            UserAddress userAddress = _db.addresses.Where(x => x.AddressId == AddressId).FirstOrDefault();
            userAddress.StreetName = streetName;
            userAddress.HouseNumber = houseNumber;
            userAddress.City = city;
            userAddress.PhoneNumber = phoneNumber;
            userAddress.PostalCode = postalCode;
            ApplicationUser user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.Zipcode = postalCode;
            _db.Users.Update(user);
            _db.addresses.Update(userAddress);
            _db.SaveChanges();
            return true;
        }
        /*Change Password*/
        public async Task<IdentityResult> ChnagePassword(string CurrentPassword, string NewPassword)
        {
            var userId = _userServicesHelper.getUserId();
            var user =await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
        }
    }
}
