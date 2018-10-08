using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XBitApi.Models;
using XBitApi.EF;
using Microsoft.AspNetCore.Authorization;

namespace XBitApi.Controllers
{
    [Controller]
    public class UserInformationController : Controller
    {
        private XBitContext context;
        private RoleHelper roleHelper;

        public UserInformationController(XBitContext context)
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

        // GET pai/userinformation
        [HttpGet]
        [Authorize(Roles = "CanReadUserInformation")]
        [Route("api/UserInformation")]
        public IActionResult GetUserInformations(string name, string surname, string email, string phone, DateTime birthDate, string username)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                List<UserInformation> userInformations;
                if (roleHelper.IsUserAdmin(currentUserId))
                {
                    userInformations = context.UserInformations.ToList();
                }
                else
                {
                    userInformations = context.UserInformations.Where(x => x.Id == currentUserId).ToList();
                }
                 

                if (!String.IsNullOrEmpty(name))
                {
                    List<UserInformation> uiToRemove = new List<UserInformation>(userInformations.Where(ui => ui.Name != name));
                    foreach (var ui in uiToRemove)
                    {
                        userInformations.Remove(ui);
                    }
                }

                if (!String.IsNullOrEmpty(surname))
                {
                    List<UserInformation> uiToRemove = new List<UserInformation>(userInformations.Where(ui => ui.Surname != surname));
                    foreach (var ui in uiToRemove)
                    {
                        userInformations.Remove(ui);
                    }
                }

                if (!String.IsNullOrEmpty(email))
                {
                    List<UserInformation> uiToRemove = new List<UserInformation>(userInformations.Where(ui => ui.Email != email));
                    foreach (var ui in uiToRemove)
                    {
                        userInformations.Remove(ui);
                    }
                }

                if (!String.IsNullOrEmpty(phone))
                {
                    List<UserInformation> uiToRemove = new List<UserInformation>(userInformations.Where(ui => ui.Phone != phone));
                    foreach (var ui in uiToRemove)
                    {
                        userInformations.Remove(ui);
                    }
                }

                if (!String.IsNullOrEmpty(username))
                {
                    List<UserInformation> uiToRemove = new List<UserInformation>(userInformations.Where(ui => ui.Username != username));
                    foreach (var ui in uiToRemove)
                    {
                        userInformations.Remove(ui);
                    }
                }

                if (birthDate != null)
                {
                    List<UserInformation> uiToRemove = new List<UserInformation>(userInformations.Where(ui => ui.BirthDate != birthDate));
                    foreach (var ui in uiToRemove)
                    {
                        userInformations.Remove(ui);
                    }
                }

                return Ok(userInformations);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/userinformation/0000-00000-0000000
        [HttpGet]
        [Authorize(Roles = "CanReadUserInformation")]
        [Route("api/UserInformation/{id}")]
        public IActionResult GetUserInformation(Guid id)
        {
            try
            {
                UserInformation userInformation = context.UserInformations.Find(id);
                if (userInformation == null)
                    return NotFound();
                if (userInformation.Id == GetCurrentUserId() || roleHelper.IsUserAdmin(GetCurrentUserId()))
                {
                    return Ok(userInformation);
                }
                return BadRequest("Access denied");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/userinformation
        [HttpPost]
        [Authorize(Roles = "CanUpdateUserInformation")]
        [Route("api/UserInformation")]
        public IActionResult PostUserInformation([FromBody]UserInformation userInformation)
        {
            try
            {
                context.UserInformations.Add(userInformation);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, userInformation);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/userinformation
        [HttpPut]
        [Authorize(Roles = "CanUpdateUserInformation")]
        [Route("api/UserInformation")]
        public IActionResult PutUserInformation([FromBody]UserInformation userInformation)
        {
            try
            {
                if (!context.UserInformations.Any(ui => ui.Id == userInformation.Id))
                    return NotFound();

                context.UserInformations.Update(userInformation);
                context.SaveChanges();
                return Ok(userInformation);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/userinformation/0000-00000-000000
        [HttpDelete]
        [Authorize(Roles = "CanDeleteUserInformation")]
        [Route("api/UserInformation/{id}")]
        public IActionResult DeleteUserInformation(Guid id)
        {
            try
            {
                UserInformation userInformation = context.UserInformations.Find(id);
                if (userInformation == null)
                    return NotFound();
                context.UserInformations.Remove(userInformation);
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
