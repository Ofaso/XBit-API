using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XBitApi.Models.ViewModel
{
    public class UserRolesVM
    {
        public string UserInformationId { get; set; }
        public List<RolesVM> Roles { get; set; }
    }
}
