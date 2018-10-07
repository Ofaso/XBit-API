using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XBitApi.Models;
using XBitApi.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace XBitApi.Controllers
{
    [Controller]
    public class HostingPeriodController : Controller
    {
        private XBitContext context;
        private RoleHelper roleHelper;

        public HostingPeriodController(XBitContext context)
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

        // GET api/hostingperiod
        [HttpGet]
        [Authorize(Roles = "CanReadHostingPeriod")]
        [Route("api/HostingPeriod")]
        public IActionResult GetHostingPeriods(DateTime startDate, DateTime endDate, Guid minerId, double pricePerMonth)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                List<HostingPeriod> hostingPeriods;
                if (roleHelper.IsUserAdmin(currentUserId))
                {
                    hostingPeriods = context.HostingPeriods.ToList();
                }
                else
                {
                    hostingPeriods = context.HostingPeriods.Include(x => x.Miner.MiningFarm).Where(x => x.Miner.MiningFarm.AdminCustomerId == currentUserId).ToList();
                }

                if (startDate != null)
                {
                    List<HostingPeriod> hpToRemove = new List<HostingPeriod>(hostingPeriods.Where(hp => hp.StartDate != startDate));
                    foreach (var hp in hpToRemove)
                    {
                        hostingPeriods.Remove(hp);
                    }
                }

                if (endDate != null)
                {
                    List<HostingPeriod> hpToRemove = new List<HostingPeriod>(hostingPeriods.Where(hp => hp.EndDate != endDate));
                    foreach (var hp in hpToRemove)
                    {
                        hostingPeriods.Remove(hp);
                    }
                }

                if (minerId != Guid.Empty)
                {
                    List<HostingPeriod> hpToRemove = new List<HostingPeriod>(hostingPeriods.Where(hp => hp.MinerId != minerId));
                    foreach (var hp in hpToRemove)
                    {
                        hostingPeriods.Remove(hp);
                    }
                }

                if (pricePerMonth != 0)
                {
                    List<HostingPeriod> hpToRemove = new List<HostingPeriod>(hostingPeriods.Where(hp => hp.PricePerMonth != pricePerMonth));
                    foreach (var hp in hpToRemove)
                    {
                        hostingPeriods.Remove(hp);
                    }
                }

                return Ok(hostingPeriods);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/hostingperiod/000-0000-000000
        [HttpGet]
        [Authorize(Roles = "CanReadHostingPeriod")]
        [Route("api/HostingPeriod/{id}")]
        public IActionResult GetHostingPeriod(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                HostingPeriod hostingPeriod;
                if (roleHelper.IsUserAdmin(currentUserId))
                {
                    hostingPeriod = context.HostingPeriods.Find(id);
                }
                else
                {
                    hostingPeriod = context.HostingPeriods.Include(x => x.Miner.MiningFarm).Where(x => x.Miner.MiningFarm.AdminCustomerId == currentUserId && x.Id == id).SingleOrDefault();
                }
             
                if (hostingPeriod == null)
                    return NotFound();
                return Ok(hostingPeriod);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/hostingPeriod
        [HttpPost]
        [Authorize(Roles = "CanUpdateHostingPeriod")]
        [Route("api/HostingPeriod")]
        public IActionResult PostHostingPeriod([FromBody]HostingPeriod hostingPeriod)
        {
            try
            {
                if (context.Miners.Find(hostingPeriod.Id) == null)
                    return BadRequest();

                context.HostingPeriods.Add(hostingPeriod);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, hostingPeriod);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/hostingPeriod
        [HttpPut]
        [Authorize(Roles = "CanUpdateHostingPeriod")]
        [Route("api/HostingPeriod")]
        public IActionResult PutHostingPeriod([FromBody]HostingPeriod hostingPeriod)
        {
            try
            {
                if (context.Miners.Find(hostingPeriod.Id) == null)
                    return BadRequest();

                if (!context.HostingPeriods.Any(hp => hp.Id == hostingPeriod.Id))
                    return NotFound();

                context.HostingPeriods.Update(hostingPeriod);
                context.SaveChanges();
                return Ok(hostingPeriod);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/hostingperiod/0000-0000-0000000
        [HttpDelete]
        [Authorize(Roles = "CanDeleteHostingPeriod")]
        [Route("api/HostingPeriod")]
        public IActionResult DeleteHostingPeriod(Guid id)
        {
            try
            {
                HostingPeriod hostingPeriod = context.HostingPeriods.Find(id);
                if (hostingPeriod == null)
                    return NotFound();
                return Ok(hostingPeriod);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
