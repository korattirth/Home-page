using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BookNowController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookNowController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult BookNow()
        {
            return View();
        }
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
    }
}
