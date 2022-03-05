using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;

        public DashboardController(ApplicationDbContext db, IUserServicesHelper userServicesHelper)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult showDetails()
        {
            var userid = _userServicesHelper.getUserId();
            List<ServicesRequest> sr = new List<ServicesRequest>();
            sr = _db.ServicesRequests.Where(x => x.UserId == userid).ToList();
            ServiceRequestAddress requestAddress = new ServiceRequestAddress();
            List<ServiceRequestAddress> servicesRequestAddress = new List<ServiceRequestAddress>();
            foreach (var request in sr)
            {
                requestAddress = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == request.ServiceRequestId).FirstOrDefault();
                servicesRequestAddress.Add(requestAddress);
            }
            dynamic model = new ExpandoObject();
            model.serviceRequest = sr;
            model.servicesAddress = servicesRequestAddress;
            return View("Index" ,model);
        }
        public List<ServiceRequestAddress> test1()
        {
            var userid = _userServicesHelper.getUserId();
            List<ServicesRequest> sr = new List<ServicesRequest>();
            sr = _db.ServicesRequests.Where(x => x.UserId == userid).ToList();
            List<ServiceRequestAddress> servicesRequestAddress = new List<ServiceRequestAddress>();
            foreach (var request in sr)
            {
                servicesRequestAddress = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == request.ServiceRequestId).ToList();
            }
            return servicesRequestAddress;
        }
    }
}
