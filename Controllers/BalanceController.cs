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
    public class BalanceController : Controller
    {
        private XBitContext context;
        private RoleHelper roleHelper;

        public BalanceController(XBitContext context)
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
        // GET api/balance
        [HttpGet]
        [Authorize(Roles = "CanReadBalance")]
        [Route("api/Balance")]
        public IActionResult GetBalances(Guid miningFarmId, Guid coinId)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                List<Balance> balances;
                if (roleHelper.IsUserAdmin(currentUserId))
                {
                    balances = context.Balances.ToList();
                }
                else
                {
                    balances = context.Balances.Include(x => x.MiningFarm).Where(x => x.MiningFarm.AdminCustomerId == currentUserId).ToList();
                }
                
                if (miningFarmId != Guid.Empty)
                {
                    List<Balance> balancesToRemove = new List<Balance>(balances.Where(ba => ba.MiningFarmId != miningFarmId));
                    foreach (var balance in balancesToRemove)
                    {
                        balances.Remove(balance);
                    }
                }

                if (coinId != Guid.Empty)
                {
                    List<Balance> balancesToRemove = new List<Balance>(balances.Where(ba => ba.CoinId != coinId));
                    foreach (var balance in balancesToRemove)
                    {
                        balances.Remove(balance);
                    }
                }

                return Ok(balances);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/balance/0000-0000-0000000
        [HttpGet("{id}")]
        [Authorize(Roles = "CanReadBalance")]
        [Route("api/Balance/{id}")]
        public IActionResult GetBalance(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                Balance balance;
                if (roleHelper.IsUserAdmin(currentUserId))
                {
                     balance = context.Balances.Find(id);
                }
                else
                {
                    balance = context.Balances.Include(x => x.MiningFarm).Where(x => x.MiningFarm.AdminCustomerId == currentUserId && x.Id == id).SingleOrDefault();
                }
                
                if (balance == null)
                    return NotFound();
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/balance
        [HttpPost]
        [Authorize(Roles = "CanUpdateBalance")]
        [Route("api/Balance")]
        public IActionResult PostBalance([FromBody]Balance balance)
        {
            try
            {
                if (context.Coins.Find(balance.CoinId) == null || context.MiningFarms.Find(balance.MiningFarmId) == null)
                    return BadRequest();

                context.Balances.Add(balance);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/balance
        [HttpPut]
        [Authorize(Roles = "CanUpdateBalance")]
        [Route("api/Balance")]
        public IActionResult PutBalance([FromBody]Balance balance)
        {
            try
            {
                if (context.Coins.Find(balance.CoinId) == null || context.MiningFarms.Find(balance.MiningFarmId) == null)
                    return BadRequest();

                if (!context.Balances.Any(ba => ba.Id == balance.Id))
                    return NotFound();

                context.Balances.Update(balance);
                context.SaveChanges();
                return Ok(context);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/balance/0000-0000-0000000
        [HttpDelete]
        [Authorize(Roles = "CanDeleteBalance")]
        [Route("api/Balance/{id}")]
        public IActionResult DeleteBalance(Guid id)
        {
            try
            {
                Balance balance = context.Balances.Find(id);
                if (balance == null)
                    return NotFound();

                context.Balances.Remove(balance);
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
