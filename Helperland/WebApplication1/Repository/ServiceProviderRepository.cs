using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Models.ViewsModel;

namespace WebApplication1.Repository
{
    public class ServiceProviderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        public ServiceProviderRepository(ApplicationDbContext db, IUserServicesHelper userServicesHelper , UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
            _userManager = userManager;
        }
        public List<NewServicesRequestViewModel> GetnewServicesRequests()
        {
            var userid = _userServicesHelper.getUserId();
            List<NewServicesRequestViewModel> nsr = new List<NewServicesRequestViewModel>();
            List<ServicesRequest> sr = new List<ServicesRequest>();
            sr = _db.ServicesRequests.Where(x => x.Status == 0 && x.ServicesProviderId == null).ToList();
            foreach (var item in sr)
            {
                FavoriteAndBlocked favoriteAndBlocked = _db.FavoriteAndBlockeds.Where(x => x.TargetUserId == item.UserId && x.IsBlocked == true).FirstOrDefault();
                if(favoriteAndBlocked == null)
                {
                    NewServicesRequestViewModel l = new NewServicesRequestViewModel();
                    l.ServicesRequestId = item.ServiceRequestId;
                    l.ServicesStartDate = item.ServiceStartDate;
                    l.Payment = item.TotalCost;
                    ApplicationUser user = _db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                    l.FIrstName = user.FirstName;
                    l.LastName = user.LastName;
                    ServiceRequestAddress serviceRequestAddress = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == item.ServiceRequestId).FirstOrDefault();
                    l.HouseNumber = serviceRequestAddress.AddressLine1;
                    l.StreetName = serviceRequestAddress.AddressLine2;
                    l.Postalcode = serviceRequestAddress.Zipcode;
                    l.City = serviceRequestAddress.city;
                    l.Status = item.Status;
                    nsr.Add(l);
                } 
            }
            return nsr;
        }
        public List<NewServicesRequestViewModel> GetUpcomingServicesRequests()

        {
            var userid = _userServicesHelper.getUserId();
            List<NewServicesRequestViewModel> nsr = new List<NewServicesRequestViewModel>();
            List<ServicesRequest> sr = new List<ServicesRequest>();
            sr = _db.ServicesRequests.Where(x => x.Status == 3 && x.ServicesProviderId == userid).ToList();
            foreach (var item in sr)
            {
                NewServicesRequestViewModel l = new NewServicesRequestViewModel();
                l.ServicesRequestId = item.ServiceRequestId;
                l.ServicesStartDate = item.ServiceStartDate;
                l.Payment = item.TotalCost;
                ApplicationUser user = _db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                l.FIrstName = user.FirstName;
                l.LastName = user.LastName;
                ServiceRequestAddress serviceRequestAddress = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == item.ServiceRequestId).FirstOrDefault();
                l.HouseNumber = serviceRequestAddress.AddressLine1;
                l.StreetName = serviceRequestAddress.AddressLine2;
                l.Postalcode = serviceRequestAddress.Zipcode;
                l.City = serviceRequestAddress.city;
                l.Status = item.Status;
                nsr.Add(l);
            }
            return nsr;
        }
        public List<NewServicesRequestViewModel> GetServiceHistory()
        {
            var userid = _userServicesHelper.getUserId();
            List<NewServicesRequestViewModel> nsr = new List<NewServicesRequestViewModel>();
            List<ServicesRequest> sr = new List<ServicesRequest>();
            sr = _db.ServicesRequests.Where(x => x.Status == 1 && x.ServicesProviderId == userid).ToList();
            foreach (var item in sr)
            {
                NewServicesRequestViewModel l = new NewServicesRequestViewModel();
                l.ServicesRequestId = item.ServiceRequestId;
                l.ServicesStartDate = item.ServiceStartDate;
                l.Payment = item.TotalCost;
                ApplicationUser user = _db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                l.FIrstName = user.FirstName;
                l.LastName = user.LastName;
                ServiceRequestAddress serviceRequestAddress = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == item.ServiceRequestId).FirstOrDefault();
                l.HouseNumber = serviceRequestAddress.AddressLine1;
                l.StreetName = serviceRequestAddress.AddressLine2;
                l.Postalcode = serviceRequestAddress.Zipcode;
                l.City = serviceRequestAddress.city;
                l.Status = item.Status;
                nsr.Add(l);
            }
            return nsr;
        }
        public List<RatingViewModel> GetRatings()
        {
            List<RatingViewModel> ratingViewModels = new List<RatingViewModel>();
            var userId = _userServicesHelper.getUserId();
            List<ServicesRating> sr =_db.ServicesRatings.Where(x => x.RatingTo == userId).ToList();
            foreach(var item in sr)
            {
                RatingViewModel ob = new RatingViewModel();
                ob.ServicesRequestId = item.ServicesrequestId;
                ApplicationUser applicationUser = _db.Users.Where(x => x.Id == item.RatingFrom).FirstOrDefault();
                ob.FirstName = applicationUser.FirstName;
                ob.LastName = applicationUser.LastName;
                ob.RatingDate = item.RatingDate;
                ob.Rating = item.Ratings;
                ob.Comments = item.Comments;
                ratingViewModels.Add(ob);
            }
            return ratingViewModels;
            
        }
        public List<BlockUserViewModel> GetBlockUser()
        {
            List<BlockUserViewModel> applicationUsers = new List<BlockUserViewModel>();
            var userId = _userServicesHelper.getUserId();
            List<ServicesRequest> sr = _db.ServicesRequests.Where(x => x.ServicesProviderId == userId && x.Status == 1).ToList();
            foreach(var item in sr)
            {
                ApplicationUser user = _db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                BlockUserViewModel blockUser = new BlockUserViewModel();
                blockUser.FirstName = user.FirstName;
                blockUser.LastName = user.LastName;
                blockUser.UserId = user.Id;
                FavoriteAndBlocked favoriteAndBlocked = _db.FavoriteAndBlockeds.Where(x => x.TargetUserId == item.UserId).FirstOrDefault();
                if(favoriteAndBlocked != null)
                {
                    blockUser.IsBlock = favoriteAndBlocked.IsBlocked;
                }
                bool alreadyExists = applicationUsers.Any(x => x.UserId == item.UserId);
                if (!alreadyExists)
                {
                    applicationUsers.Add(blockUser);
                }
            }
            return applicationUsers;
        }
        public ServicesProviderSettingViewModel GetSetting()
        {
            ServicesProviderSettingViewModel setting = new ServicesProviderSettingViewModel();
            var userId = _userServicesHelper.getUserId();
            ApplicationUser user =_db.Users.Where(x => x.Id == userId).FirstOrDefault();
            setting.FirstName = user.FirstName;
            setting.LastName = user.LastName;
            setting.UserID = userId;
            setting.PhoneNumber = user.PhoneNumber;
            setting.DateOfBirth = user.DateOfBirth;
            setting.Email = user.Email;
            setting.Nationality = user.NationalityId;
            setting.Gender = user.Gender;
            setting.Avtar = user.UserProfilepicture;
            setting.ActiveStatus = user.status;
            UserAddress address = _db.addresses.Where(x => x.UserId == userId).FirstOrDefault();
            if(address == null)
            {
                return setting;
            }
            else
            {
                setting.StreetName = address.StreetName;
                setting.HouseNumber = address.HouseNumber;
                setting.Postalcode = address.PostalCode;
                setting.City = address.City;
                return setting;
            }
        }
        public object Details(int RequestId)
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
            return obj;
        }
        public bool acceptRequest(int servicesRequestId)
        {
        /*Status 0 intial , 1 Complete , 2 cancel ,3 Upcoming*/
            ServicesRequest sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == servicesRequestId).FirstOrDefault();
            sr.Status = 3;
            var userId = _userServicesHelper.getUserId();
            ApplicationUser applicationUser = _db.Users.Where(x => x.Id == userId && x.UserTypeId == 2).FirstOrDefault();
            if(applicationUser == null)
            {
                return false;
            }
            else
            {
                sr.ServicesProviderId = applicationUser.Id;
                _db.ServicesRequests.Update(sr);
                _db.SaveChanges();
                return true;
            }
        }
        public bool CancelRequest(int servicesRequestId)
        {
            ServicesRequest sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == servicesRequestId).FirstOrDefault();
            sr.Status = 2;
            _db.ServicesRequests.Update(sr);
            _db.SaveChanges();
            return true;
        }
        public bool CompleteRequest(int servicesRequestId)
        {
            ServicesRequest sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == servicesRequestId).FirstOrDefault();
            sr.Status = 1;
            _db.ServicesRequests.Update(sr);
            _db.SaveChanges();
            return true;
        }
        public bool UpdateDetails(string firstName,string lastName ,string phoneNumber,string email ,DateTime dateOfbirth ,string streetName ,string houseNumber ,string city ,int postalCode ,int nationality,int gender, string avtar, string userId)
        {
            ApplicationUser user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Gender = gender;
            user.Email = email;
            user.NationalityId = nationality;
            user.DateOfBirth = dateOfbirth.ToString("yyyy/MM/dd");
            user.PhoneNumber = phoneNumber;
            user.UserProfilepicture = avtar.ToString();
            user.Zipcode = postalCode;
            _db.Users.Update(user);
            _db.SaveChanges();
            UserAddress userAddress = _db.addresses.Where(x => x.UserId == userId).FirstOrDefault();
            if(userAddress != null)
            {
                userAddress.Email = email;
                userAddress.City = city;
                userAddress.StreetName = streetName;
                userAddress.HouseNumber = houseNumber;
                userAddress.PostalCode = postalCode;
                _db.addresses.Update(userAddress);
                _db.SaveChanges();
            }
            else
            {
                UserAddress address = new UserAddress();
                address.Email = email;
                address.City = city;
                address.StreetName = streetName;
                address.HouseNumber = houseNumber;
                address.PostalCode = postalCode;
                address.PhoneNumber = Double.Parse(phoneNumber);
                address.UserId = userId;
                _db.addresses.Add(address);
                _db.SaveChanges();
            }
            return true;
        }
        public async Task<IdentityResult> ChnagePassword(string CurrentPassword, string NewPassword)
        {
            var userId = _userServicesHelper.getUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
        }
        public bool BlockCustomer(string userId)
        {
            FavoriteAndBlocked Blocked = _db.FavoriteAndBlockeds.Where(x => x.TargetUserId == userId).FirstOrDefault();
            if(Blocked == null)
            {
                ServicesRequest sr = _db.ServicesRequests.Where(x => x.UserId == userId).FirstOrDefault();
                FavoriteAndBlocked favoriteAndBlocked = new FavoriteAndBlocked();
                favoriteAndBlocked.UserID = sr.ServicesProviderId;
                favoriteAndBlocked.TargetUserId = userId;
                favoriteAndBlocked.IsBlocked = true;
                _db.FavoriteAndBlockeds.Add(favoriteAndBlocked);
                _db.SaveChanges();
                return true;
            }
            else
            {
                Blocked.IsBlocked = true;
                _db.FavoriteAndBlockeds.Update(Blocked);
                _db.SaveChanges();
                return true;
            }
            
        }
        public bool UnBlockCustomer(string userId)
        {
            FavoriteAndBlocked favoriteAndBlocked = _db.FavoriteAndBlockeds.Where(x => x.TargetUserId == userId).FirstOrDefault();
            favoriteAndBlocked.IsBlocked = false;
            _db.FavoriteAndBlockeds.Update(favoriteAndBlocked);
            _db.SaveChanges();
            return true;
        }
    }
}
