using Microsoft.AspNetCore.Mvc;
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
    public class IBookServicesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServicesHelper _userServicesHelper;

        public IBookServicesRepository(ApplicationDbContext db, IUserServicesHelper userServicesHelper)
        {
            _db = db;
            _userServicesHelper = userServicesHelper;
        }
        public List<UserAddress> GetAddresses()
        {
            var userid = _userServicesHelper.getUserId();
            List<UserAddress> addresses = _db.addresses.Where(x => x.UserId == userid).ToList();
            return addresses;
        }
        public int GetServicesRequest(BookServicesViewModel bookServicesViewModel)
        {
            var userid = _userServicesHelper.getUserId();
            ServicesRequest sr = new ServicesRequest();
            sr.UserId = userid;
            sr.ServiceStartDate = bookServicesViewModel.ServicesStartDate.ToString("yyyy/MM/dd");
            sr.Zipcode = bookServicesViewModel.zipCode;
            sr.ServiceHourlyrate = 18;
            sr.ServicesHours = float.Parse(bookServicesViewModel.ServicesHours);
            sr.Comments = bookServicesViewModel.Comments;
            sr.CraetedDate = DateTime.Now;
            sr.ModifiedDate = DateTime.Now;
            sr.PaymentDone = true;
            sr.Status = 0;
            ApplicationUser user = _db.Users.Where(x => x.Id == userid).FirstOrDefault();
            sr.Zipcode = user.Zipcode;
            sr.HasPets = bookServicesViewModel.HasPets;
            decimal totalCost = 54;
            double forExtra = 0;
            if (bookServicesViewModel.InsideCabinet)
            {
                forExtra += 0.5;
                totalCost += 9;
            }
            if (bookServicesViewModel.InsideFridge)
            {
                forExtra += 0.5;
                totalCost += 9;
            }
            if (bookServicesViewModel.InterioOven)
            {
                forExtra += 0.5;
                totalCost += 9;
            }
            if (bookServicesViewModel.InteriorWindows)
            {
                forExtra += 0.5;
                totalCost += 9;
            }
            if (bookServicesViewModel.LaundryWashDry)
            {
                forExtra += 0.5;
                totalCost += 9;
            }
            sr.ExtraHours = (float)forExtra;
            sr.TotalCost = totalCost;
            _db.ServicesRequests.Add(sr);
            _db.SaveChanges();

            UserAddress address = _db.addresses.Where(x => x.AddressId == bookServicesViewModel.AddressId).FirstOrDefault();
            var email = _db.Users.Where(x => x.Id == userid).Select(x => new { Email = x.Email }).Single();
            ServiceRequestAddress serviceRequestAddress = new ServiceRequestAddress()
            {
                ServicesRequestId = sr.ServiceRequestId,
                AddressLine1 = address.HouseNumber,
                AddressLine2 = address.StreetName,
                city = address.City,
                Zipcode = address.PostalCode,
                PhoneNumber = address.PhoneNumber,
                Email = email.Email
            };
            _db.ServiceRequestAddress.Add(serviceRequestAddress);
            _db.SaveChanges();

            if (bookServicesViewModel.InsideCabinet)
            {
                ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra()
                {
                    ServicesExtraId = 1,
                    ServicesRequestId = sr.ServiceRequestId
                };
                _db.ServiceRequestExtras.Add(serviceRequestExtra);
                _db.SaveChanges();
            }
            if (bookServicesViewModel.InsideFridge)
            {
                ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra()
                {
                    ServicesExtraId = 2,
                    ServicesRequestId = sr.ServiceRequestId
                };
                _db.ServiceRequestExtras.Add(serviceRequestExtra);
                _db.SaveChanges();
            }
            if (bookServicesViewModel.InterioOven)
            {
                ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra()
                {
                    ServicesExtraId = 3,
                    ServicesRequestId = sr.ServiceRequestId
                };
                _db.ServiceRequestExtras.Add(serviceRequestExtra);
                _db.SaveChanges();
            }
            if (bookServicesViewModel.InteriorWindows)
            {
                ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra()
                {
                    ServicesExtraId = 4,
                    ServicesRequestId = sr.ServiceRequestId
                };
                _db.ServiceRequestExtras.Add(serviceRequestExtra);
                _db.SaveChanges();
            }
            if (bookServicesViewModel.LaundryWashDry)
            {
                ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra()
                {
                    ServicesExtraId = 5,
                    ServicesRequestId = sr.ServiceRequestId
                };
                _db.ServiceRequestExtras.Add(serviceRequestExtra);
                _db.SaveChanges();
            }

            return sr.ServiceRequestId;
        }
    }
}
