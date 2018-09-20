using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XBitApi.Models.ViewModel
{
    public class UserClaimRolesVM
    {
        public int Id { get; set; }
        public Guid UserInformationId { get; set; }
        public int ClaimRolesId { get; set; }
    }
}
