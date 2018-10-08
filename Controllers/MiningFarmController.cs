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
    public class MiningFarmController : Controller
    {
        private XBitContext context;
        private RoleHelper roleHelper;

        public MiningFarmController(XBitContext context)
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

        // GET api/miningfarm
        [HttpGet]
        [Authorize(Roles = "CanReadMiningFarm")]
        [Route("api/MiningFarm")]
        public IActionResult GetMiningFarms(string name, Guid adminCustomerId)
        {
            try
            {
                if (GetCurrentUserId().Equals(adminCustomerId) || roleHelper.IsUserAdmin(GetCurrentUserId()))
                {


                    List<MiningFarm> miningFarms = context.MiningFarms.ToList();
                    if (!String.IsNullOrEmpty(name))
                    {
                        List<MiningFarm> mfToRemove = new List<MiningFarm>(miningFarms.Where(mf => mf.Name != name));
                        foreach (var mf in mfToRemove)
                        {
                            miningFarms.Remove(mf);
                        }
                    }

                    if (adminCustomerId != Guid.Empty)
                    {
                        List<MiningFarm> mfToRemove = new List<MiningFarm>(miningFarms.Where(mf => mf.AdminCustomerId != adminCustomerId));
                        foreach (var mf in mfToRemove)
                        {
                            miningFarms.Remove(mf);
                        }
                    }

                    return Ok(miningFarms);
                }
                return BadRequest("Access Denied");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/miningfarm/0000-0000-00000
        [HttpGet]
        [Authorize(Roles = "CanReadMiningFarm")]
        [Route("api/MiningFarm/{id}")]
        public IActionResult GetMiningFarm(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                MiningFarm miningFarm;
                if (roleHelper.IsUserAdmin(currentUserId))
                {
                    miningFarm = context.MiningFarms.Find(id);
                }
                else
                {
                    miningFarm = context.MiningFarms.Where(x => x.AdminCustomerId == currentUserId && x.Id == id).SingleOrDefault();
                }
                if (miningFarm == null)
                    return NotFound();
                return Ok(miningFarm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/miningfarm
        [HttpPost]
        [Authorize(Roles = "CanUpdateMiningFarm")]
        [Route("api/MiningFarm")]
        public IActionResult PostMiningFarm([FromBody]MiningFarm miningFarm)
        {
            try
            {
                if (context.Users.Find(miningFarm.AdminCustomerId) == null)
                    return BadRequest();

                context.MiningFarms.Add(miningFarm);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, miningFarm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/miningFarm
        [HttpPut]
        [Authorize(Roles = "CanUpdateMiningFarm")]
        [Route("api/MiningFarm")]
        public IActionResult PutMiningFarm([FromBody]MiningFarm miningFarm)
        {
            try
            {
                if (context.Users.Find(miningFarm.AdminCustomerId) == null)
                    return BadRequest();

                if (!context.MiningFarms.Any(mf => mf.Id == miningFarm.Id))
                    return NotFound();

                context.MiningFarms.Update(miningFarm);
                context.SaveChanges();
                return Ok(miningFarm);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/miningfarm/000-0000-000000
        [HttpDelete]
        [Authorize(Roles = "CanDeleteMiningFarm")]
        [Route("api/MiningFarm/{id}")]
        public IActionResult DeleteMiningFarm(Guid id)
        {
            try
            {
                MiningFarm miningFarm = context.MiningFarms.Find(id);
                if (miningFarm == null)
                    return NotFound();
                context.MiningFarms.Remove(miningFarm);
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
