using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XBitApi.Models;

namespace XBitApi.EF
{
    public static class XBitDBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.
                GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                XBitContext context = serviceScope.ServiceProvider.GetService<XBitContext>();

                if (!context.Claims.Any())
                {
                    List<Claims> claims = new List<Claims>() {
                        new Claims(){ Claim= "CanUpdateMiner" },
                        new Claims(){ Claim= "CanDeleteMiner" },
                        new Claims(){ Claim= "CanReadMiner" },
                        new Claims(){ Claim= "CanUpdateAddress" },
                        new Claims(){ Claim= "CanDeleteAddress" },
                        new Claims(){ Claim= "CanReadAddress" },
                        new Claims(){ Claim= "CanUpdateAlgorithm" },
                        new Claims(){ Claim= "CanDeleteAlgorithm" },
                        new Claims(){ Claim= "CanReadAlgorithm" },
                        new Claims(){ Claim= "CanUpdateBalance" },
                        new Claims(){ Claim= "CanDeleteBalance" },
                        new Claims(){ Claim= "CanReadBalance" },
                        new Claims(){ Claim= "CanUpdateCoinAlgorithm" },
                        new Claims(){ Claim= "CanDeleteCoinAlgorithm" },
                        new Claims(){ Claim= "CanReadCoinAlgorithm" },
                        new Claims(){ Claim= "CanUpdateCoin" },
                        new Claims(){ Claim= "CanReadCoin" },
                        new Claims(){ Claim= "CanDeleteCoin" },
                        new Claims(){ Claim= "CanUpdateCountry" },
                        new Claims(){ Claim= "CanDeleteCountry" },
                        new Claims(){ Claim= "CanReadCountry" },
                        new Claims(){ Claim= "CanUpdateFarmMember" },
                        new Claims(){ Claim= "CanDeleteFarmMember" },
                        new Claims(){ Claim= "CanReadFarmMember" },
                        new Claims(){ Claim= "CanUpdateFarmRight" },
                        new Claims(){ Claim= "CanDeleteFarmRight" },
                        new Claims(){ Claim= "CanReadFarmRight" },
                        new Claims(){ Claim= "CanUpdateHostingPeriod" },
                        new Claims(){ Claim= "CanDeleteHostingPeriod" },
                        new Claims(){ Claim= "CanReadHostingPeriod" },
                        new Claims(){ Claim= "CanReadLocation" },
                        new Claims(){ Claim= "CanUpdateLocation" },
                        new Claims(){ Claim= "CanDeleteLocation" },
                        new Claims(){ Claim= "CanUpdateManufacturer" },
                        new Claims(){ Claim= "CanDeleteManufacturer" },
                        new Claims(){ Claim= "CanReadManufacturer" },
                        new Claims(){ Claim= "CanReadMinerAlgorithm" },
                        new Claims(){ Claim= "CanUpdateMinerAlgorithm" },
                        new Claims(){ Claim= "CanDeleteMinerAlgorithm" },
                        new Claims(){ Claim= "CanUpdateMinerType" },
                        new Claims(){ Claim= "CanDeleteMinerType" },
                        new Claims(){ Claim= "CanReadMinerType" },
                        new Claims(){ Claim= "CanUpdateMiningFarm" },
                        new Claims(){ Claim= "CanDeleteMiningFarm" },
                        new Claims(){ Claim= "CanReadMiningFarm" },
                        new Claims(){ Claim= "CanUpdateShelf" },
                        new Claims(){ Claim= "CanDeleteShelf" },
                        new Claims(){ Claim= "CanReadShelf" },
                        new Claims(){ Claim= "CanReadUser" },
                        new Claims(){ Claim= "CanUpdateUser" },
                        new Claims(){ Claim= "CanDeleteUser" },
                        new Claims(){ Claim= "CanReadUserInformation" },
                        new Claims(){ Claim= "CanUpdateUserInformation" },
                        new Claims(){ Claim= "CanDeleteUserInformation" }

                    };
                    context.Claims.AddRange(claims);

                    List<Roles> roles = new List<Roles>() {
                        new Roles(){ Role = "Admin" },
                        new Roles(){ Role = "Customer" }
                    };
                    
                    context.Roles.AddRange(roles);
                    context.SaveChanges();


                    //Get Role Id for Admin
                    var AdminRole = roles.FirstOrDefault(p => p.Role.Equals("Admin"));
                    List<ClaimRoles> adminClaimRoles = new List<ClaimRoles>();
                    foreach (var claim in claims)
                    {
                        adminClaimRoles.Add(new ClaimRoles() { ClaimsId = claim.Id, RolesId = AdminRole.Id });
                    }

                    context.ClaimRoles.AddRange(adminClaimRoles);
                    context.SaveChanges();

                    List<string> customerClaims = new List<string>() { "CanReadMiner", "CanReadUser", "CanReadMiningFarm", "CanReadBalance", "CanReadHostingPeriod", "CanReadUserInformation", "CanReadCoin", "CanReadAlgorithm", "CanReadMinerType", "CanReadManufacturer", "CanReadMinerAlgorithm", "CanReadCountry" };
                    var CustomerRole = roles.FirstOrDefault(p => p.Role.Equals("Customer"));
                    List<ClaimRoles> customerClaimRoles = new List<ClaimRoles>();
                    foreach (var claim in claims)
                    {
                        if (customerClaims.FirstOrDefault(p => p.Equals(claim.Claim)) != null) {
                            customerClaimRoles.Add(new ClaimRoles() { ClaimsId = claim.Id, RolesId = CustomerRole.Id });
                        }
                    }

                    context.ClaimRoles.AddRange(customerClaimRoles);
                    context.SaveChanges();

                }
                
            }
        }
    }
}
