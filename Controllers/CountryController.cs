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
    //[Route("api/[controller]")] please make sure to follow the same syntax of using the controller
    // and methods as in the Address...
    public class CountryController : Controller
    {
        private XBitContext context;

        public CountryController(XBitContext context)
        {
            this.context = context;
        }

        // GET api/country
        [HttpGet]
        [Authorize(Roles = "CanReadCountry")]
        [Route("api/Country/{name}")]
        public IActionResult GetCountries(string name)
        {
            try
            {
                List<Country> countries = context.Countries.ToList();

                if (!String.IsNullOrEmpty(name))
                {
                    List<Country> countriesWithoutName = new List<Country>(countries.Where(country => country.Name != name));
                    foreach (Country country in countriesWithoutName)
                    {
                        countries.Remove(country);
                    }
                }

                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        // GET api/country/Guid
        [HttpGet]
        [Authorize(Roles = "CanReadCountry")]
        [Route("api/Country/{id}")]
        public IActionResult GetCountry(Guid id)
        {
            try
            {
                Country country = context.Countries.Find(id);
                if (country == null)
                {
                    return NotFound();
                }
                return Ok(country);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/country
        [HttpPost]
        [Authorize(Roles = "CanUpdateCountry")]
        [Route("api/Country")]
        public IActionResult PostCountry([FromBody]Country country)
        {
            try
            {
                context.Countries.Add(country);
                context.SaveChanges();
                string url = Url.ActionContext.HttpContext.Request.Path;
                return Created(url, country);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PUT api/country
        [HttpPut]
        [Authorize(Roles = "CanUpdateCountry")]
        [Route("api/Country")]
        public IActionResult PutCountry([FromBody]Country country)
        {
            try
            {
                if (context.Countries.Any(co => co.Id == country.Id))
                {
                    context.Countries.Update(country);
                    context.SaveChanges();
                    return Ok(country);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/country/id
        [HttpDelete]
        [Authorize(Roles = "CanDeleteCountry")]
        [Route("api/Country/{id}")]
        public IActionResult DeleteCountry(Guid id)
        {
            try
            {
                Country countryToRemove = context.Countries.Find(id);
                if (countryToRemove != null)
                {
                    context.Countries.Remove(countryToRemove);
                    context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("api/Country/MinerCount")]
        public IActionResult GetMinerCount(Guid id)
        {
            try
            {
                int count = context.Miners.Where(x => x.Shelf.Location.Address.CountryId == id).Count();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("api/Country/LocationCount")]
        public IActionResult GetLocationCount(Guid id)
        {
            try
            {
                int count = context.Locations.Where(x => x.Address.CountryId == id).Count();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
