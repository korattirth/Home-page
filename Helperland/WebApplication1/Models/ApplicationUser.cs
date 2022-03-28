using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Zipcode { get; set; }
        public int UserTypeId { get; set; }
        public string DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int RoleId { get; set; }
        public string Website { get; set; }
        public string UserProfilepicture { get; set; }
        public bool isRagisteredUser { get; set; }
        public string PaymentgatewayUserref { get; set; }
        public bool WorkWithPets { get; set; }
        public int LanguageId { get; set; }
        public int NationalityId { get; set; }
        public string ResetKey { get; set; }
        public string createdDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool isApproved { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int status { get; set; }
        public bool isOnline { get; set; }
        public string BankTokenId { get; set; }
        public string TokenNo { get; set; }
    }
}
