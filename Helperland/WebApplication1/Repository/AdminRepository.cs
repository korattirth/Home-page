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
    public class AdminRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;
        private readonly SendEmail _sendEmail;

        public AdminRepository(ApplicationDbContext db, IUserServicesHelper userServicesHelper , SendEmail sendEmail)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
            _sendEmail = sendEmail;
        }
        public List<ServiceRequestAdminViewModel> GetServiceRequest()
        {
            List<ServicesRequest> sr = _db.ServicesRequests.ToList();
            List<ServiceRequestAdminViewModel> AdminRequest = new List<ServiceRequestAdminViewModel>();
            foreach(var item in sr)
            {
                ServiceRequestAdminViewModel sa = new ServiceRequestAdminViewModel();
                sa.ServicesRequestId = item.ServiceRequestId;
                sa.ServiceStartDate = item.ServiceStartDate;
                sa.NetAmount = item.TotalCost;
                sa.Status = item.Status;
                ApplicationUser Customer = _db.Users.Where(x => x.Id == item.UserId && x.UserTypeId == 3).FirstOrDefault();
                sa.FirstName = Customer.FirstName;
                sa.LastName = Customer.LastName;
                ApplicationUser Provider = _db.Users.Where(x => x.Id == item.ServicesProviderId && x.UserTypeId == 2).FirstOrDefault();
                if(Provider != null)
                {
                    sa.ProviderFirstName = Provider.FirstName;
                    sa.ProviderLastName = Provider.LastName;
                    sa.Avtar = Provider.UserProfilepicture;
                }
                ServiceRequestAddress address = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == item.ServiceRequestId).FirstOrDefault();
                sa.HouseNumber = address.AddressLine1;
                sa.StreetName = address.AddressLine2;
                sa.City = address.city;
                sa.Postalcode = address.Zipcode;
                ServicesRating rating = _db.ServicesRatings.Where(x => x.ServicesrequestId == item.ServiceRequestId).FirstOrDefault();
                if(rating != null)
                {
                    sa.Rating = rating.Ratings;
                }
                AdminRequest.Add(sa);
            }
            return AdminRequest;
        }
        public List<ServiceRequestAdminViewModel> SerchRequest(ServicesHistoryAdminViewModel list)
        {
            List<ServicesRequest> sr = _db.ServicesRequests.ToList();
            List<ServiceRequestAdminViewModel> te = new List<ServiceRequestAdminViewModel>();
            SerchViewModel serch = list.SerchHistory;
            if(serch.ServicerequestId != 0)
            {
                sr = sr.Where(x => x.ServiceRequestId == serch.ServicerequestId).ToList();
            }
            if(serch.PostalCode != 0)
            {
                sr = sr.Where(x => x.Zipcode.ToString().Contains(serch.PostalCode.ToString(), System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if(serch.Status != -1)
            {
                sr = sr.Where(x => x.Status == serch.Status).ToList();
            }
            if(serch.FromDate != null)
            {
                var fromDate = Convert.ToDateTime(serch.FromDate);
                sr = sr.Where(x => Convert.ToDateTime(x.ServiceStartDate) > fromDate).ToList();
            }
            if (serch.Todate != null)
            {
                var toDate = Convert.ToDateTime(serch.Todate);
                sr = sr.Where(x => Convert.ToDateTime(x.ServiceStartDate) < toDate).ToList();
            }
            bool cheak = false;
            bool inSide = false;
            foreach (var item in sr)
            {
                cheak = false;
                ServiceRequestAdminViewModel sa = new ServiceRequestAdminViewModel();
                sa.ServicesRequestId = item.ServiceRequestId;
                sa.ServiceStartDate = item.ServiceStartDate;
                sa.NetAmount = item.TotalCost;
                sa.Status = item.Status;
                ApplicationUser Customer = _db.Users.Where(x => x.Id == item.UserId && x.UserTypeId == 3).FirstOrDefault();
                sa.FirstName = Customer.FirstName;
                sa.LastName = Customer.LastName;
                ApplicationUser Provider = _db.Users.Where(x => x.Id == item.ServicesProviderId && x.UserTypeId == 2).FirstOrDefault();
                if (Provider != null)
                {
                    sa.ProviderFirstName = Provider.FirstName;
                    sa.ProviderLastName = Provider.LastName;
                    sa.Avtar = Provider.UserProfilepicture;
                }
                ServiceRequestAddress address = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == item.ServiceRequestId).FirstOrDefault();
                sa.HouseNumber = address.AddressLine1;
                sa.StreetName = address.AddressLine2;
                sa.City = address.city;
                sa.Postalcode = address.Zipcode;
                ServicesRating rating = _db.ServicesRatings.Where(x => x.ServicesrequestId == item.ServiceRequestId).FirstOrDefault();
                if (rating != null)
                {
                    sa.Rating = rating.Ratings;
                }
                if (serch.Email != null)
                {
                    inSide = true;
                    if (Customer.Email.Contains(serch.Email, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        cheak = true;
                    }
                }
                if (serch.Customer != null)
                {
                    inSide = true;
                    if (Customer.FirstName.Contains(serch.Customer, System.StringComparison.CurrentCultureIgnoreCase) || Customer.LastName.Contains(serch.Customer, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        cheak = true;
                    }
                }
                if (serch.ServiceProvider != null)
                {
                    if(sa.ProviderFirstName != null)
                    {
                        inSide = true;
                        if (Provider.FirstName.Contains(serch.ServiceProvider, System.StringComparison.CurrentCultureIgnoreCase) || Provider.LastName.Contains(serch.ServiceProvider, System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            cheak = true;
                        }
                    }
                }
                if (cheak == true && inSide == true)
                {
                    te.Add(sa);
                }
                if (!inSide)
                {
                    te.Add(sa);
                }
            }
            return te;
        }
        public object getDetails(int ServiceRequestId)
        {
           ServicesRequest sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == ServiceRequestId).FirstOrDefault();
            ServiceRequestAdminViewModel sra = new ServiceRequestAdminViewModel();
            sra.ServicesRequestId = sr.ServiceRequestId;
            sra.ServiceStartDate = sr.ServiceStartDate;
            ServiceRequestAddress address = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == ServiceRequestId).FirstOrDefault();
            sra.HouseNumber = address.AddressLine1;
            sra.StreetName = address.AddressLine2;
            sra.City = address.city;
            sra.Postalcode = address.Zipcode;
            return sra;
        }
        public bool updateSchedule(DateTime ServiceStartDate,string StreetName,string HouseNumber,string City,int Postalcode,string invoiceStreetName,string invoiceHouseNumber,string invoiceCity,int invoicePostalcode,int ServiceRequestId)
        {
            ServicesRequest sr = _db.ServicesRequests.Where(x => x.ServiceRequestId == ServiceRequestId).FirstOrDefault();
            sr.ServiceStartDate = ServiceStartDate.ToString("yyyy/MM/dd");
            ServiceRequestAddress address = _db.ServiceRequestAddress.Where(x => x.ServicesRequestId == ServiceRequestId).FirstOrDefault();
            address.AddressLine1 = HouseNumber;
            address.AddressLine2 = StreetName;
            address.Zipcode = Postalcode;
            address.city = City;
            _db.ServicesRequests.Update(sr);
            _db.ServiceRequestAddress.Update(address);
            _db.SaveChanges();
            ApplicationUser user = _db.Users.Where(x => x.Id == sr.UserId).FirstOrDefault();
            if(sr.ServicesProviderId != null)
            {
                ApplicationUser ServiceProvider = _db.Users.Where(x => x.Id == sr.ServicesProviderId).FirstOrDefault();
                string body1 = "Message From HelperLand <br/> Your Shchedule is Updated Please cheak once Your Shcedule";
                string To1 = user.Email;
                string subject1 = "Shcedule Updated";
                _sendEmail.SendMail(To1, subject1, body1);
            }
            string body = "Message From HelperLand <br/> Your Shchedule is Updated Please cheak once Your Shcedule";
            string To = user.Email;
            string subject = "Shcedule Updated";
            _sendEmail.SendMail(To, subject, body);
            return true;
        }
        public List<ApplicationUser> SerchUserManagement(UserManagementAdminViewModel list)
        {
            SerchUserManagement serchUserManagement = list.serchUserManagement;
            List<ApplicationUser> user = _db.Users.ToList();
            if(serchUserManagement.UserName != null)
            {
               user = user.Where(x => x.FirstName.Contains(serchUserManagement.UserName, System.StringComparison.CurrentCultureIgnoreCase) || x.LastName.Contains(serchUserManagement.UserName, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if(serchUserManagement.Email != null)
            {
                user = user.Where(x => x.Email.Contains(serchUserManagement.Email, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if(serchUserManagement.PostalCode != 0)
            {
                user = user.Where(x => x.Zipcode.ToString().Contains(serchUserManagement.PostalCode.ToString(), System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (serchUserManagement.FromDate != null)
            {
                var fromDate = Convert.ToDateTime(serchUserManagement.FromDate);
                user = user.Where(x => Convert.ToDateTime(x.createdDate) > fromDate).ToList();
            }
            if (serchUserManagement.ToDate != null)
            {
                var toDate = Convert.ToDateTime(serchUserManagement.ToDate);
                user = user.Where(x => Convert.ToDateTime(x.createdDate) < toDate).ToList();
            }
            if (serchUserManagement.PhoneNumber != 0)
            {
                user = user.Where(x => x.PhoneNumber.Contains(serchUserManagement.PhoneNumber.ToString(), System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (serchUserManagement.Usertype != 0)
            {
                user = user.Where(x => x.UserTypeId == serchUserManagement.Usertype).ToList();
            }
            return user;
        }
        public bool activeAccount(string UserId)
        {
            ApplicationUser user = _db.Users.Where(x => x.Id == UserId).FirstOrDefault();
            user.status = 1;
            _db.Users.Update(user);
            _db.SaveChanges();
            return true;
        }
        public bool deActiveAccount(string UserId)
        {
            ApplicationUser user = _db.Users.Where(x => x.Id == UserId).FirstOrDefault();
            user.status = 2;
            _db.Users.Update(user);
            _db.SaveChanges();
            return true;
        }

    }
}
