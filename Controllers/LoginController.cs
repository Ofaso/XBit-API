﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using XBitApi.EF;
using XBitApi.Models;
using XBitApi.Provider.JWT;
using XBitApi.Utilities;

namespace XBit_Api.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private XBitContext context;

        public LoginController(XBitContext context)
        {
            this.context = context;
        }

        [Route("api/Login/Create")]
        [HttpPost]
        public IActionResult Create([FromBody]LoginModel loginModel)
        {
            List<string> ClaimsList = new List<string>();
            if (!ModelState.IsValid) {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(allErrors);
            }
            else{
                UserInformation userInfo = context.UserInformations.Include(p=>p.UserClaimsRoles)
                                           .FirstOrDefault(p => p.Username.Equals(loginModel.UserName));
                if (userInfo != null)
                {
                    User userDetails = context.Users.FirstOrDefault(p => 
                                               p.UserInformationId.ToString().Equals(userInfo.Id.ToString()) 
                                               && p.Password.Equals(Hashing.sha256_hash(loginModel.Password)));

                    //Get User Claims according to there roles.
                    foreach (var userClaimRole in userInfo.UserClaimsRoles)
                    {
                        ClaimRoles claimRole = context.ClaimRoles.Include(p => p.Claims)
                                              .Include(q=>q.Roles)
                                              .FirstOrDefault(p => p.Id == userClaimRole.ClaimRolesId);
                        if (claimRole != null)
                        {
                          ClaimsList.Add(claimRole.Claims.Claim);
                        }
                    }

                    if (userDetails != null)
                    {
                        var token = new JwtTokenBuilder()
                                                .AddSecurityKey(JwtSecurityKey.Create("XBitApi-secret-key-1234"))
                                                .AddSubject(userInfo.Name)
                                                .AddIssuer("XBitApi.Security.Bearer")
                                                .AddAudience("XBitApi.Security.Bearer")
                                                .AddClaim("UserId", userInfo.Id.ToString())
                                                .AddClaimRoles(ClaimsList)
                                                .AddExpiry(30)
                                                .Build();
                        
                        return Ok(token.Value);
                    }
                    else {
                        return BadRequest(new { Error = "Invalid Username or password!" });
                    }
                }
                else {
                    return BadRequest(new { Error = "Invalid Username or password!" });
                }
                
            }  
        }
    }
}