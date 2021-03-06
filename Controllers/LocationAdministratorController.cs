﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XBitApi.Models;
using XBitApi.EF;

namespace XBitApi.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class LocationAdministratorController : Controller
    {
        private XBitContext context;

        public LocationAdministratorController(XBitContext context)
        {
            this.context = context;
        }

        // GET api/locationadministrator
        [HttpGet]
        public IActionResult GetLocationAdministrators(Guid locationId, Guid administratorId)
        {
            try
            {
                List<LocationAdministrator> locationAdministrators = context.LocationAdministrators.ToList();

                if (locationId != Guid.Empty)
                {
                    List<LocationAdministrator> laToRemove = new List<LocationAdministrator>(locationAdministrators.Where(la => la.LocationId != locationId));
                    foreach (var la in laToRemove)
                    {
                        locationAdministrators.Remove(la);
                    }
                }

                if (administratorId != Guid.Empty)
                {
                    List<LocationAdministrator> laToRemove = new List<LocationAdministrator>(locationAdministrators.Where(la => la.AdministratorId != administratorId));
                    foreach (var la in laToRemove)
                    {
                        locationAdministrators.Remove(la);
                    }
                }

                return Ok(locationAdministrators);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/locationadministrator/0000-00000-00000000
        [HttpGet("{id}")]
        public IActionResult GetLocationAdministrator(Guid id)
        {
            try
            {
                LocationAdministrator locationAdministrator = context.LocationAdministrators.Find(id);
                if (locationAdministrator == null)
                    return NotFound();
                return Ok(locationAdministrator);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/locationAdministrator
        [HttpPost]
        public IActionResult PostLocationAdministrator([FromBody]LocationAdministrator locationAdministrator)
        {
            try
            {
                if (context.Locations.Find(locationAdministrator.LocationId) == null || context.Administrators.Find(locationAdministrator.AdministratorId) == null)
                    return BadRequest();

                context.LocationAdministrators.Add(locationAdministrator);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, locationAdministrator);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/locationAdministrator
        [HttpPut]
        public IActionResult PutLocationAdministrator([FromBody]LocationAdministrator locationAdministrator)
        {
            try
            {
                if (context.Locations.Find(locationAdministrator.LocationId) == null || context.Administrators.Find(locationAdministrator.AdministratorId) == null)
                    return BadRequest();

                context.LocationAdministrators.Update(locationAdministrator);
                context.SaveChanges();
                return Ok(locationAdministrator);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/locationadministrator
        [HttpDelete("{id}")]
        public IActionResult DeleteLocationAdministrator(Guid id)
        {
            try
            {
                LocationAdministrator locationAdministrator = context.LocationAdministrators.Find(id);
                if (locationAdministrator == null)
                    return NotFound();

                context.LocationAdministrators.Remove(locationAdministrator);
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
