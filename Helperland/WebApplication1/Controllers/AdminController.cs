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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;
        private readonly AdminRepository _adminrepository;

        public AdminController(ApplicationDbContext db, IUserServicesHelper userServicesHelper,AdminRepository adminrepository)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
            _adminrepository = adminrepository;
        }
        public IActionResult ServiceRequest()
        {
            ServicesHistoryAdminViewModel servicesHistoryAdminViewModel = new ServicesHistoryAdminViewModel();
            servicesHistoryAdminViewModel.serviceRequestAdmin = _adminrepository.GetServiceRequest();
            return View(servicesHistoryAdminViewModel);
        }
        [HttpPost]
        public IActionResult ServiceRequest(ServicesHistoryAdminViewModel serch)
        {
            ServicesHistoryAdminViewModel servicesHistoryAdminViewModel = new ServicesHistoryAdminViewModel();
            if(serch.SerchHistory == null)
            {
                servicesHistoryAdminViewModel.serviceRequestAdmin = _adminrepository.GetServiceRequest();
            }
            else
            {
                servicesHistoryAdminViewModel.serviceRequestAdmin = _adminrepository.SerchRequest(serch);
            }
            return View(servicesHistoryAdminViewModel);
        }
        public IActionResult GetDetails(int ServicerequestId)
        {
            var sra = _adminrepository.getDetails(ServicerequestId);
            return Json(sra);
        }
        public bool UpdateSchedule(DateTime ServiceStartDate, string StreetName, string HouseNumber, string City, int Postalcode, string invoiceStreetName, string invoiceHouseNumber, string invoiceCity, int invoicePostalcode, int ServiceRequestId)
        {
            return _adminrepository.updateSchedule(ServiceStartDate, StreetName, HouseNumber, City, Postalcode, invoiceStreetName, invoiceHouseNumber, invoiceCity, invoicePostalcode, ServiceRequestId);
        }
        public IActionResult UserMangement()
        {
            UserManagementAdminViewModel userManagementAdmin = new UserManagementAdminViewModel();
            userManagementAdmin.User = _db.Users.ToList();
            return View(userManagementAdmin);
        }
        [HttpPost]
        public IActionResult UserMangement(UserManagementAdminViewModel userManagementAdmin)
        {
            UserManagementAdminViewModel userManagement = new UserManagementAdminViewModel();
            if (userManagementAdmin.serchUserManagement == null)
            {
                userManagement.User = _db.Users.ToList();
            }
            else
            {
                userManagement.User = _adminrepository.SerchUserManagement(userManagementAdmin);
            }
            return View(userManagement);
        }
        public bool ActiveAccount(string UserId)
        {
            _adminrepository.activeAccount(UserId);
            return true;
        }
        public bool DeActiveAccount(string UserId)
        {
            _adminrepository.deActiveAccount(UserId);
            return true;
        }
    }
}
