using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XBitApi.EF;
using XBitApi.Models;

namespace XBitApi
{
    public class RoleHelper
    {
        private XBitContext context;
        public RoleHelper(XBitContext context)
        {
            this.context = context;
        }

        private List<ClaimRoles> GetClaimRolesByUserInformationId(Guid userinformationId)
        {
            List<ClaimRoles> claimsRoles = new List<ClaimRoles>();
            UserInformation userInfo = context.UserInformations.Include(p => p.UserClaimsRoles)
                                           .FirstOrDefault(p => p.Id == userinformationId);
            foreach (var userClaimRole in userInfo.UserClaimsRoles)
            {
                ClaimRoles claimRole = context.ClaimRoles.Include(p => p.Claims)
                                      .Include(q => q.Roles)
                                      .FirstOrDefault(p => p.Id == userClaimRole.Id);
                claimsRoles.Add(claimRole);
            }
            return claimsRoles;
        }

        public bool IsUserAdmin(Guid userinformationId)
        {
            List<ClaimRoles> claimroles = GetClaimRolesByUserInformationId(userinformationId);
            if (claimroles.Any(x => x.Roles.Role == RoleType.Admin))
            {
                return true;
            }
            return false;
        }

        public bool IsUserCustomer(Guid userinformationId)
        {
            List<ClaimRoles> claimroles = GetClaimRolesByUserInformationId(userinformationId);
            if (claimroles.Any(x => x.Roles.Role == RoleType.Customer))
            {
                return true;
            }
            return false;
        }
    }
}
