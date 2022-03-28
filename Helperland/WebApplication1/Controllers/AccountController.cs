using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SendEmail _sendEmail;
        private readonly RandomPwGenarate _randomPwGenarate;
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,SendEmail sendEmail , RandomPwGenarate randomPwGenarate)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _sendEmail = sendEmail;
            _randomPwGenarate = randomPwGenarate;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RagisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber.ToString(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NationalityId = 1,
                    LanguageId = 1,
                    UserTypeId = 3,
                    status = 1,
                    createdDate = DateTime.Now.ToString("yyyy/MM/dd")
                };
                var results = await _userManager.CreateAsync(user, model.PassWord);

                if (results.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    ApplicationUser applicationUser = _db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                    HttpContext.Session.SetInt32("usertypeId", applicationUser.UserTypeId);

                    string body = "Welcome To HelperLand <br/> Your SignUp is Successfull";
                    string To = user.Email;
                    string subject = "SignUp SuccessFull";
                    _sendEmail.SendMail(To, subject, body);
                    return RedirectToAction("Index","Home");
                }
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.isLogin = true;
            return View("~/Views/Home/Index.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _db.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                    if(user.status == 1)
                    {
                        var results = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                        if (results.Succeeded)
                        {
                            HttpContext.Session.SetInt32("usertypeId", user.UserTypeId);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<bool> ForgetPassword(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return false;
            }
            else
            {
                string pw = _randomPwGenarate.GeneratePassword(true, true, true, true, 8);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetResult = await _userManager.ResetPasswordAsync(user, code, pw);
               
                string body = "Welcome To HelperLand <br/> Your Password is" + pw;
                string To = user.Email;
                string subject = "Forget Password";
                _sendEmail.SendMail(To, subject, body);
                return true;
            }
        }
        public async Task<IActionResult> SignUpServiceProvider(RagisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber.ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalityId = 1,
                UserProfilepicture = "one",
                LanguageId = 1,
                createdDate = DateTime.Now.ToString("yyyy/MM/dd"),
                status = 2,
                UserTypeId = 2
            };
            var results = await _userManager.CreateAsync(user, model.PassWord);
            if (results.Succeeded)
            {
                string body = "Welcome To HelperLand <br/> Your Ragistration is Successfull When Our System Are accept Your Request After That You Can Able To Login";
                string To = user.Email;
                string subject = "Ragistration SuccessFull";
                _sendEmail.SendMail(To, subject, body);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("BecomeAHelper", "Home");
            }
        }
    }
}
