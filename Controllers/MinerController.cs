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
    [Route("api/[controller]")]
    public class MinerController : Controller
    {
        private XBitContext context;

        public MinerController(XBitContext context)
        {
            this.context = context;
        }

        private Guid? GetCurrentUserId()
        {
            var currentUser = User.Claims.FirstOrDefault(p => p.Type.Equals("UserId"));
            if (currentUser != null)
            {
                return new Guid(currentUser.Value);
            }
            return null;
        }

        // GET api/manufacturer
        [HttpGet]
        public IActionResult GetMiners (Guid minerTypeId, Guid coinAlgorithmId, Guid miningFarmId, Guid shelfId)
        {
            try
            {
                List<Miner> miners = context.Miners.ToList();

                if (minerTypeId != Guid.Empty)
                {
                    List<Miner> miToRemove = new List<Miner>(miners.Where(mi => mi.MinerTypeId != minerTypeId));
                    foreach (var mi in miToRemove)
                    {
                        miners.Remove(mi);
                    }
                }

                if (coinAlgorithmId != Guid.Empty)
                {
                    List<Miner> miToRemove = new List<Miner>(miners.Where(mi => mi.CoinAlgorithmId != coinAlgorithmId));
                    foreach (var mi in miToRemove)
                    {
                        miners.Remove(mi);
                    }
                }

                if (miningFarmId != Guid.Empty)
                {
                    List<Miner> miToRemove = new List<Miner>(miners.Where(mi => mi.MiningFarmId != miningFarmId));
                    foreach (var mi in miToRemove)
                    {
                        miners.Remove(mi);
                    }
                }

                if (shelfId != Guid.Empty)
                {
                    List<Miner> miToRemove = new List<Miner>(miners.Where(mi => mi.ShelfId != shelfId));
                    foreach (var mi in miToRemove)
                    {
                        miners.Remove(mi);
                    }
                }

                return Ok(miners);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/miner/000-000-000000
        [HttpGet]
        [Authorize(Roles = "CanReadOwnMiner")]
        [Route("api/Address/Miner/{id}")]
        public IActionResult GetMiner(Guid id)
        {
            try
            {
                Miner miner = context.Miners.Find(id);
                if (miner.MiningFarm.AdminCustomerId == GetCurrentUserId())
                {
                    ;
                }
                
                if (miner == null)
                    return NotFound();
                return Ok(miner);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/miner
        [HttpPost]
        public IActionResult PostMiner([FromBody]Miner miner)
        {
            try
            {
                if (context.MinerTypes.Find(miner.MinerTypeId) == null || context.CoinAlgorithms.Find(miner.CoinAlgorithmId) == null || context.MiningFarms.Find(miner.MiningFarmId) == null || context.Shelves.Find(miner.ShelfId) == null)
                    return BadRequest();

                context.Miners.Add(miner);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, miner);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/miner
        [HttpPut]
        public IActionResult PutMiner([FromBody]Miner miner)
        {
            try
            {
                if (context.MinerTypes.Find(miner.MinerTypeId) == null || context.CoinAlgorithms.Find(miner.CoinAlgorithmId) == null || context.MiningFarms.Find(miner.MiningFarmId) == null || context.Shelves.Find(miner.ShelfId) == null)
                    return BadRequest();

                if (!context.Miners.Any(mi => mi.Id == miner.Id))
                    return NotFound();

                context.Miners.Update(miner);
                context.SaveChanges();
                return Ok(miner);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/miner/00000-00000-0000000
        [HttpDelete("{id}")]
        public IActionResult DeleteMiner(Guid id)
        {
            try
            {
                Miner miner = context.Miners.Find(id);
                if (miner == null)
                    return NotFound();

                context.Miners.Remove(miner);
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
