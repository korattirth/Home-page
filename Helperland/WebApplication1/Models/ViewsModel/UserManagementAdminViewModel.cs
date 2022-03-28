using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewsModel
{
    public class UserManagementAdminViewModel
    {
        public IEnumerable<ApplicationUser> User { get;set; }
        public SerchUserManagement serchUserManagement { get; set; }

    }
}
