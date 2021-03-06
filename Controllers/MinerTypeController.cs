﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XBitApi.Models;
using XBitApi.EF;
using Microsoft.AspNetCore.Authorization;

namespace XBitApi.Controllers
{
    [Controller]
    public class MinerTypeController : Controller
    {
        private XBitContext context;

        public MinerTypeController(XBitContext context)
        {
            this.context = context;
        }

        // GET api/minertype
        [HttpGet]
        [Authorize(Roles = "CanReadMinerType")]
        [Route("api/MinerType")]
        public IActionResult GetMinerTypes(string name, Guid manufacturerId)
        {
            try
            {
                List<MinerType> minerTypes = context.MinerTypes.ToList();

                if (!String.IsNullOrEmpty(name))
                {
                    List<MinerType> mtToRemove = new List<MinerType>(minerTypes.Where(mt => mt.Name != name));
                    foreach (var mt in mtToRemove)
                    {
                        minerTypes.Remove(mt);
                    }
                }

                if (manufacturerId != Guid.Empty)
                {
                    List<MinerType> mtToRemove = new List<MinerType>(minerTypes.Where(mt => mt.ManufacturerId != manufacturerId));
                    foreach (var mt in mtToRemove)
                    {
                        minerTypes.Remove(mt);
                    }
                }

                return Ok(minerTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/minertype/000-0000-000000
        [HttpGet]
        [Authorize(Roles = "CanReadMinerType")]
        [Route("api/MinerType/{id}")]
        public IActionResult GetMinerTypes(Guid id)
        {
            try
            {
                MinerType minerType = context.MinerTypes.Find(id);
                if (minerType == null)
                    return NotFound();

                return Ok(minerType);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/minertype
        [HttpPost]
        [Authorize(Roles = "CanUpdateMinerType")]
        [Route("api/MinerType")]
        public IActionResult PostMinerType([FromBody]MinerType minerType)
        {
            try
            {
                if (context.Manufacturers.Find(minerType.ManufacturerId) == null)
                    return BadRequest();

                context.MinerTypes.Add(minerType);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, minerType);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/minertype
        [HttpPut]
        [Authorize(Roles = "CanUpdateMinerType")]
        [Route("api/MinerType")]
        public IActionResult PutMinerType([FromBody]MinerType minerType)
        {
            try
            {
                if (context.Manufacturers.Find(minerType.ManufacturerId) == null)
                    return BadRequest();

                if (!context.MinerTypes.Any(mt => mt.Id == minerType.Id))
                    return NotFound();

                context.MinerTypes.Update(minerType);
                context.SaveChanges();
                return Ok(minerType);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/minertype/000-0000-0000000
        [HttpDelete]
        [Authorize(Roles = "CanDeleteMinerType")]
        [Route("api/MinerType/{id}")]
        public IActionResult DeleteMinerType(Guid id)
        {
            try
            {
                MinerType minerType = context.MinerTypes.Find(id);
                if (minerType == null)
                    return NotFound();

                context.MinerTypes.Remove(minerType);
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
