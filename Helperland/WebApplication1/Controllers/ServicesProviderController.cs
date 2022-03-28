using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Models.ViewsModel;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ServicesProviderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;
        private readonly ServicesRequestRepository _servicesRequestRepository;
        private readonly RatingsRepository _ratingsRepository;
        private readonly ServiceProviderRepository _serviceProviderRepository;

        public ServicesProviderController(ApplicationDbContext db, IUserServicesHelper userServicesHelper, ServicesRequestRepository servicesRequestRepository, RatingsRepository ratingsRepository ,ServiceProviderRepository serviceProviderRepository)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
            _servicesRequestRepository = servicesRequestRepository;
            _ratingsRepository = ratingsRepository;
            _serviceProviderRepository = serviceProviderRepository;
        }
        public IActionResult NewServicesRequest()
        {
            List<NewServicesRequestViewModel> newServicesRequests = _serviceProviderRepository.GetnewServicesRequests();
            return View(newServicesRequests);
        }
        public IActionResult UpcomingServicesRequest()
        {
            List<NewServicesRequestViewModel> UpcomingServiceRequest = _serviceProviderRepository.GetUpcomingServicesRequests();
            return View(UpcomingServiceRequest);
        }
        public IActionResult ServicesHistory()
        {
            List<NewServicesRequestViewModel> newServicesRequests = _serviceProviderRepository.GetServiceHistory();
            return View(newServicesRequests);
        }
        public IActionResult MyRatting()
        {
            List<RatingViewModel> ratingViewModels = _serviceProviderRepository.GetRatings();
            return View(ratingViewModels);
        }
        public IActionResult BlockUser()
        {
            List<BlockUserViewModel> applicationUsers = _serviceProviderRepository.GetBlockUser();
            return View(applicationUsers);
        }
        public IActionResult MySetting()
        {
            ServicesProviderSettingViewModel sr = _serviceProviderRepository.GetSetting();
            return View(sr);
        }
        public IActionResult GetServicesDetails(int servicesRequestId)
        {
            return Json(_serviceProviderRepository.Details(servicesRequestId));
        }
        public bool AcceptRequest(int servicesRequestId)
        {
            return _serviceProviderRepository.acceptRequest(servicesRequestId);
        }
        public bool CancelRequest(int servicesRequestId)
        {
            return _serviceProviderRepository.CancelRequest(servicesRequestId);
        }
        public bool CompleteRequest(int servicesRequestId)
        {
            return _serviceProviderRepository.CompleteRequest(servicesRequestId);
        }
        public bool UpdateDetails(string firstName, string lastName, string phoneNumber, string email, DateTime dateOfbirth, string streetName, string houseNumber, string city, int postalCode, int nationality, int gender, string avtar, string userId)
        {
            return _serviceProviderRepository.UpdateDetails(firstName,lastName,phoneNumber,email,dateOfbirth,streetName,houseNumber, city, postalCode,nationality,gender,avtar,userId);
        }
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
        public bool BlockCustomer(string userId)
        {
            return _serviceProviderRepository.BlockCustomer(userId);
        }
        public bool UnBlockCustomer(string userId)
        {
            return _serviceProviderRepository.UnBlockCustomer(userId);
        }
    }
}
