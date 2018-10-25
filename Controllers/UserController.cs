using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XBitApi.Models;
using XBitApi.EF;
using XBitApi.Models.ViewModel;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using XBitApi.Utilities;

namespace XBitApi.Controllers
{
    [Controller]
    //[Route("api/[controller]")]
    public class UserController : Controller
    { 
        private XBitContext context;
        private RoleHelper roleHelper;

        public UserController(XBitContext context)
        {
            this.context = context;
            roleHelper = new RoleHelper(context);
        }

        private Guid GetCurrentUserId()
        {
            var currentUser = User.Claims.FirstOrDefault(p => p.Type.Equals("UserId"));
            if (currentUser != null)
            {
                return new Guid(currentUser.Value);
            }
            return new Guid();
        }

        // GET api/User
        [HttpGet]
        [Authorize(Roles = "CanReadUser")]
        [Route("api/User")]
        public IActionResult GetUsers(Guid userInformationId, Guid addressId, string farmMail)
        {
            try
            {
                
                List<User> Users = context.Users.ToList();
                Guid currentUserId = GetCurrentUserId();
                if (roleHelper.IsUserAdmin(currentUserId))
                {
                    Users = context.Users.ToList();
                }
                else
                {
                    Users = context.Users.Where(x => x.UserInformationId == currentUserId).ToList();
                }
                if (userInformationId != Guid.Empty)
                {
                    List<User> UsersToRemove = new List<User>(Users.Where(cu => cu.UserInformationId != userInformationId));
                    foreach (var User in UsersToRemove)
                    {
                        Users.Remove(User);
                    }
                }

                if (addressId != Guid.Empty)
                {
                    List<User> UsersToRemove = new List<User>(Users.Where(cu => cu.AddressId != addressId));
                    foreach (var User in UsersToRemove)
                    {
                        Users.Remove(User);
                    }
                }

                if (!String.IsNullOrEmpty(farmMail))
                {
                    List<User> UsersToRemove = new List<User>(Users.Where(cu => cu.FarmMail != farmMail));
                    foreach (var User in UsersToRemove)
                    {
                        Users.Remove(User);
                    }
                }

                return Ok(Users);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/User/0000-0000-00000000
        [HttpGet]
        [Authorize(Roles = "CanReadUser")]
        [Route("api/User/{id}")]
        public IActionResult GetUser(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                User user = context.Users.Find(id);
                if (user == null)
                    return NotFound();
                if (user.UserInformationId == currentUserId || roleHelper.IsUserAdmin(currentUserId))
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Access denied!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/User/PostUserRolesAssociation")]
        [Authorize(Roles = "CanUpdateUser")]
        public IActionResult PostUserRolesAssociation([FromBody]UserRolesVM UserRolesVM)
        {
            if (ModelState.IsValid)
            {
                List<int> ClaimRolesIds = new List<int>();
                foreach (var role in UserRolesVM.Roles)
                {
                    //check roles exist?
                    Roles rolesVM = context.Roles.FirstOrDefault(p => p.Role.ToLower().Equals(role.Role.ToLower()));
                    if (rolesVM == null)
                    {
                        var addRole = context.Roles.Add(new Roles() { Role = role.Role });
                        context.SaveChanges();
                        role.Id = addRole.Entity.Id;
                    }
                    else
                    {
                        role.Id = rolesVM.Id;
                    }

                    foreach (var claim in role.Claims)
                    {
                        //check claims exist?
                        Claims claimsVM = context.Claims.FirstOrDefault(p => p.Claim.ToLower().Equals(claim.Claim.ToLower()));
                        if (claimsVM == null)
                        {
                            var addClaim = context.Claims.Add(new Claims() { Claim = claim.Claim });
                            context.SaveChanges();
                            claim.Id = addClaim.Entity.Id;
                        }
                        else {
                            claim.Id = claimsVM.Id;
                        }

                        //check ClaimRoles exist? If not Add them...
                        ClaimRoles claimRolesVM = context.ClaimRoles.FirstOrDefault(p => p.RolesId == role.Id && p.ClaimsId == claim.Id);
                        if (claimRolesVM == null)
                        {
                            var claimRoles = context.ClaimRoles.Add(new ClaimRoles() { ClaimsId = claim.Id, RolesId = role.Id });
                            context.SaveChanges();
                            ClaimRolesIds.Add(claimRoles.Entity.Id);
                        }
                        else
                        {
                            ClaimRolesIds.Add(claimRolesVM.Id);
                        }

                    }

                    //check UserClaimRoles exist....
                    foreach (var claimRolesId in ClaimRolesIds)
                    {
                        var UserClaimRoles = context.UserClaimRoles.FirstOrDefault(p => p.ClaimRolesId == claimRolesId && p.UserInformationId == new Guid(UserRolesVM.UserInformationId));
                        if (UserClaimRoles == null) {
                            var userClaimRoles = context.UserClaimRoles.Add(new UserClaimRoles() { ClaimRolesId = claimRolesId, UserInformationId = new Guid(UserRolesVM.UserInformationId) });
                            context.SaveChanges();
                        }
                    }
                    
                }
                return Ok();
            }
            else {
                return StatusCode(500);
            }
        }

        // POST api/User
        [HttpPost]
        [Authorize(Roles = "CanUpdateUser")]
        public IActionResult PostUser([FromBody]User User)
        {
            try
            {
                if (context.UserInformations.Find(User.UserInformationId) == null || context.Addresses.Find(User.AddressId) == null)
                    return BadRequest();

                context.Users.Add(User);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, User);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/User
        [HttpPost]
        [Route("api/User/CreateUser")]
        [Authorize(Roles = "CanUpdateUser")]
        public IActionResult CreateUser([FromBody]UserVM UserVM)
        {
            try
            {
                Guid addressId = Guid.Empty;
                Guid userInformationId = Guid.Empty;
                Guid UserId = Guid.Empty;
                if (ModelState.IsValid)
                {
                    UserInformation uInformation = context.UserInformations.FirstOrDefault(p => p.Username.Equals(UserVM.Username));

                    if (uInformation == null)
                    {
                        UserInformation userInformation = new UserInformation()
                        {
                            Email = UserVM.Email,
                            BirthDate = UserVM.BirthDate,
                            Name = UserVM.Name,
                            Phone = UserVM.Phone,
                            Surname = UserVM.Surname,
                            Username = UserVM.Username,
                            Id = Guid.NewGuid()

                        };
                        var userInformationObject = context.UserInformations.Add(userInformation);
                        context.SaveChanges();
                        if (userInformationObject.Entity.Id != Guid.Empty)
                        {
                            userInformationId = userInformationObject.Entity.Id;
                            //Add Address into the system...!!!
                            Country country = new Country()
                            {
                                Name = UserVM.Address.CountryName,
                                Id = Guid.NewGuid()
                            };

                            var countrySaved = context.Countries.Add(country);
                            context.SaveChanges();
                            if (countrySaved.Entity.Id != Guid.Empty)
                            {
                                Address address = new Address()
                                {
                                    Id = Guid.NewGuid(),
                                    CountryId = countrySaved.Entity.Id,
                                    Place = UserVM.Address.Place,
                                    Street = UserVM.Address.Street,
                                    Zip = UserVM.Address.Zip
                                };

                                var addressSaved = context.Addresses.Add(address);
                                context.SaveChanges();
                                if (addressSaved.Entity.Id != Guid.Empty)
                                {
                                    addressId = addressSaved.Entity.Id;
                                }
                            }

                            User User = new User()
                            {
                                AddressId = addressId,
                                UserInformationId = userInformationId,
                                FarmMail = UserVM.Email,
                                Password = Hashing.sha256_hash(UserVM.Password),
                                Id = Guid.NewGuid()
                            };

                            var UserSaved = context.Users.Add(User);
                            context.SaveChanges();
                            if (UserSaved.Entity.Id != Guid.Empty)
                            {
                                UserId = UserSaved.Entity.Id;
                            }
                        }

                        if (UserVM.UserClaimsRoles != null && UserVM.UserClaimsRoles.Roles.Count > 0)
                        {
                            UserVM.UserClaimsRoles.UserInformationId = userInformationId.ToString();
                            IActionResult httpResponseMessage = PostUserRolesAssociation(UserVM.UserClaimsRoles);

                        }
                    }
                    else
                    {
                        return BadRequest(new { Error = "UserName Already Exist!" });
                    }
                }
                else {

                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return BadRequest(new { InvalidData = errors });
                }
                return Ok(UserId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        // PUT api/User
        [HttpPut]
        [Route("api/User")]
        [Authorize(Roles = "CanUpdateUser")]
        public IActionResult PutUser([FromBody]User User)
        {
            try
            {
                if (context.UserInformations.Find(User.UserInformationId) == null || context.Addresses.Find(User.AddressId) == null)
                    return BadRequest();

                if (context.Users.Find(User.Id) == null)
                    return NotFound();

                context.Users.Update(User);
                context.SaveChanges();
                return Ok(User);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/User/0000-00000-000000
        [HttpDelete]
        [Route("api/User/{id}")]
        [Authorize(Roles = "CanDeleteUser")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                User User = context.Users.Find(id);
                if (User == null)
                    return NotFound();

                context.Users.Remove(User);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
