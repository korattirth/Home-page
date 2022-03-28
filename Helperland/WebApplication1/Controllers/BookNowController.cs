using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class BookNowController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;
        private readonly IBookServicesRepository _bookServicesRepository;

        public BookNowController(ApplicationDbContext db , IUserServicesHelper userServicesHelper ,IBookServicesRepository bookServicesRepository)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
            _bookServicesRepository = bookServicesRepository;
        }
        [Authorize]
        public IActionResult BookNow()
        {
            return View();
        }

        /*CheakAvailablity Of Zipcode */
        [HttpPost]
        public Boolean CheakAvailablityOfZipcode(int zipcode)
        {
            if(zipcode.ToString().Length > 7)
            {
                return false;
            }
            else
            {
                ApplicationUser user = _db.Users.Where(x => x.Zipcode == zipcode && x.UserTypeId == 2).FirstOrDefault();
                if(user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /*Add Address*/
       [HttpPost]
        public bool AddAddress(string StreetName ,string HouseNumber ,double PhoneNumber, int PostalCode , string City)
        {
            var userid = _userServicesHelper.getUserId();
            {
                UserAddress address = new UserAddress()
                {
                    StreetName = StreetName,
                    HouseNumber = HouseNumber,
                    PhoneNumber = PhoneNumber,
                    PostalCode = PostalCode,
                    City = City,
                    UserId = userid,
                };
                _db.addresses.Add(address);
                _db.SaveChanges();
                return true;
            }
        }

        /*Get Address*/
        [HttpGet]
        public  IActionResult GetAddresses()
        {
            List<UserAddress> addresses = _bookServicesRepository.GetAddresses();
            if(addresses == null)
            {
                Json(false);
            }
            return Json(JsonConvert.SerializeObject(addresses));
        }

        /*Book Services*/
        public IActionResult bookServices(BookServicesViewModel bookServicesViewModel)
        {
           ViewBag.serviceRequestId = _bookServicesRepository.GetServicesRequest(bookServicesViewModel);
            ViewBag.IsSuccess = true;
            return View("BookNow");
        }
        public IActionResult GetServicesProvider(int zipcode)
        {
           List< ApplicationUser> user = _db.Users.Where(x => x.UserTypeId == 2 && x.Zipcode == zipcode).ToList();
            return Json(user);
        }
    }
}
