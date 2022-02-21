using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDbContext db,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Prices()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult BecomeAHelper()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }
        [HttpPost]
        public Boolean Test(int serch)
        {
            ApplicationUser applicationUser = _db.Users.Where(x => x.Zipcode == serch && x.UserTypeId == 2).FirstOrDefault();
            if (applicationUser == null)
                return false;
            else
                return true;
        }

    }
}
