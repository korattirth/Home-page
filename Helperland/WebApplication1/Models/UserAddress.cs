using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserAddress
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Street Name")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Please Enter City Name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter House Number")]
        public string HouseNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DataType(DataType.PhoneNumber , ErrorMessage ="Enter Valid Number")]
        public Double PhoneNumber { get; set; }
        [Required]
        [MaxLength(7,ErrorMessage ="Enter Valid Number")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public int Type { get; set; }
    }
}
