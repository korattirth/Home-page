using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Models.ViewsModel;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;
        private readonly ServicesRequestRepository _servicesRequestRepository;
        private readonly RatingsRepository _ratingsRepository;

        public DashboardController(ApplicationDbContext db, IUserServicesHelper userServicesHelper ,ServicesRequestRepository servicesRequestRepository,RatingsRepository ratingsRepository)
        {   
            _db = db;
            _userServicesHelper = userServicesHelper;
            _servicesRequestRepository = servicesRequestRepository;
            _ratingsRepository = ratingsRepository;
        }
        /*Dashboard Details*/
        public IActionResult showDetails()
        {
            List<CurrentServicesViewModel> currentServices = _servicesRequestRepository.getDetailsDashboard();
            return View("Index" , currentServices);
        }
        /*Pop Up Window Details Dashboard*/
        public IActionResult Details(int RequestId)
        {
            ServicesRequest sr = new ServicesRequest();
            sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == RequestId).FirstOrDefault();
            ServiceRequestAddress requestAddress = new ServiceRequestAddress();
            requestAddress = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == RequestId).FirstOrDefault();
            List<ServiceRequestExtra> extra = new List<ServiceRequestExtra>();
            extra = _db.ServiceRequestExtras.Where(x => x.ServicesRequestId == RequestId).ToList();
            string ExtraServices = null;
            foreach (var item in extra)
            {
                if (item.ServicesExtraId == 1)
                {
                    ExtraServices += "Inside Cabinets, ";
                }
                if (item.ServicesExtraId == 2)
                {
                    ExtraServices += "Inside Fridge, ";
                }
                if (item.ServicesExtraId == 3)
                {
                    ExtraServices += "Inside Oven, ";
                }
                if (item.ServicesExtraId == 4)
                {
                    ExtraServices += "Laundry Wash & dry, ";
                }
                if (item.ServicesExtraId == 5)
                {
                    ExtraServices += "Interior windows, ";
                }
            }
            var obj = new
            {
                HouseNumber = requestAddress.AddressLine1,
                StreetName = requestAddress.AddressLine2,
                City = requestAddress.city,
                Email = requestAddress.Email,
                PhoneNumber = requestAddress.PhoneNumber,
                RequestId = RequestId,
                ServicesStartDate = sr.ServiceStartDate,
                Duration = sr.ServicesHours,
                TotalCost = sr.TotalCost,
                HasPets = sr.HasPets,
                Extra = ExtraServices,
                Commemts = sr.Comments
            };
            return Json(obj);
        }
        /*Update Date*/
        public bool UpdateSchedule(int requestId ,DateTime UpdateDate)
        {
          return _servicesRequestRepository.UpdateServicesDate(requestId, UpdateDate);
        }
        /*Cancel Services*/
        public bool CancelServices(int requestId, string cancelReason)
        {
            return _servicesRequestRepository.CancelRequest(requestId,cancelReason);
        }
        /*Services History Details*/
        public IActionResult ServicesHistory()
        {
            List<ServicesHistoryViewModel> servicesHistoryViews = _servicesRequestRepository.GetServicesHistory();
            return View(servicesHistoryViews);
        }
        /*Pop Up Window Details For ServicesHistory*/
        public IActionResult DetailsServicesHistory(int RequestId)
        {
            ServicesRequest sr = new ServicesRequest();
            sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == RequestId).FirstOrDefault();
            ServiceRequestAddress requestAddress = new ServiceRequestAddress();
            requestAddress = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == RequestId).FirstOrDefault();
            List<ServiceRequestExtra> extra = new List<ServiceRequestExtra>();
            extra = _db.ServiceRequestExtras.Where(x => x.ServicesRequestId == RequestId).ToList();
            string ExtraServices = null;
            foreach (var item in extra)
            {
                if (item.ServicesExtraId == 1)
                {
                    ExtraServices += "Inside Cabinets, ";
                }
                if (item.ServicesExtraId == 2)
                {
                    ExtraServices += "Inside Fridge, ";
                }
                if (item.ServicesExtraId == 3)
                {
                    ExtraServices += "Inside Oven, ";
                }
                if (item.ServicesExtraId == 4)
                {
                    ExtraServices += "Laundry Wash & dry, ";
                }
                if (item.ServicesExtraId == 5)
                {
                    ExtraServices += "Interior windows, ";
                }
            }
            var obj = new
            {
                HouseNumber = requestAddress.AddressLine1,
                StreetName = requestAddress.AddressLine2,
                City = requestAddress.city,
                Email = requestAddress.Email,
                PhoneNumber = requestAddress.PhoneNumber,
                RequestId = RequestId,
                ServicesStartDate = sr.ServiceStartDate,
                Duration = sr.ServicesHours,
                TotalCost = sr.TotalCost,
                HasPets = sr.HasPets,
                Extra = ExtraServices,
                Commemts = sr.Comments,
            };
            return Json(obj);
        }
        /*Add Rating*/
        public bool AddRating(int ServicesRequestId, decimal onTime, decimal friendly, decimal qualityOfService, string feedBack)
        {
           return _ratingsRepository.Addrating(ServicesRequestId, onTime, friendly, qualityOfService, feedBack);
        }
        public IActionResult MySetting()
        {
            var userid = _userServicesHelper.getUserId();
            ApplicationUser applicationUser = _db.Users.Where(x => x.Id == userid).FirstOrDefault();
            return View(applicationUser);
        }
        public bool UpdateUserDetails(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth, int language, string userId)
        {
            return _servicesRequestRepository.UpdateuserDetails(firstName, lastName, email, phoneNumber, dateOfBirth, language, userId);
        }
        /*Get Address For My Setting*/
        public List<UserAddress> GetAddress(string userId)
        {
            List<UserAddress> address = _db.addresses.Where(x => x.UserId == userId).ToList();
            return address;
        }
        /*Add New Address*/
        public bool addAddress(string streetName, string houseNumber, string city, double phoneNumber, int postalCode, string userId)
        {
            return _servicesRequestRepository.addAddress(streetName, houseNumber, city, phoneNumber, postalCode, userId);
        }
        public JsonResult GetAddressByAddressId(int AddressId)
        {
          UserAddress userAddresses = _db.addresses.Where(x => x.AddressId == AddressId).FirstOrDefault();
            return Json(userAddresses);
        }
        /*Update Address By Address Id*/
        public bool UpdateAddress(string streetName, string houseNumber, string city, double phoneNumber, int postalCode, int AddressId)
        {
            return _servicesRequestRepository.UpdateAddress(streetName, houseNumber, city, phoneNumber, postalCode, AddressId);
        }
        /*Cancel Address by AddressId*/
        public bool CancelAddress(int AddressId)
        {
           UserAddress userAddress = _db.addresses.Where(x => x.AddressId == AddressId).FirstOrDefault();
            _db.addresses.Remove(userAddress);
            _db.SaveChanges();
            return true;
        }
        /*Change Password*/
        public async Task<bool> ChangePassword(string CurrentPassword, string NewPassword)
        {
            var result = await _servicesRequestRepository.ChnagePassword(CurrentPassword, NewPassword);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

