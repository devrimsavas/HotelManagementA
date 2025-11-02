
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models.HotelModels;
using HotelManagementA.DTOs.LocationDTOs;
using HotelManagementA.Models;
using System.Reflection.Metadata.Ecma335;
namespace HotelManagementA.Controllers.LocationController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LocationController:ControllerBase
    {
        private AppDbContext _appDbContext;
        public LocationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //get all locations 
        //conversion with Select 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationReadDTO>>> GetLocations()
        {
            
            var locations = await _appDbContext.Locations
                .Select(l => new LocationReadDTO
                {
                    Id = l.Id,
                    City = l.City,
                    PostCode=l.PostCode,
                    Street=l.Street,
                    Country=l.Country,
                })
                .ToListAsync();

            return Ok(locations);

        }

        [HttpPost] 
        public async Task<ActionResult<LocationReadDTO>> AddLocation([FromBody] LocationCreateDTO location)
        {
            if (string.IsNullOrWhiteSpace(location.City) ||  string.IsNullOrWhiteSpace(location.PostCode) ||  string.IsNullOrWhiteSpace(location.Street) )
            {
                return BadRequest(new { Message = "all fields should be filled " });
            }

            var exists = await _appDbContext.Locations.AnyAsync(l =>
                l.City.ToLower() == location.City.ToLower() &&
                l.Street.ToLower() == location.Street.ToLower() &&
                l.PostCode.ToLower() == location.PostCode.ToLower() &&
                l.Country.ToLower() == location.Country.ToLower());

            if (exists)
            {
                return Conflict(new { Message = "This location already exists." });
            }

            var newLocation = new Location
            {
                City = location.City,
                PostCode = location.PostCode,
                Street = location.Street,
                Country = location.Country
            };

            _appDbContext.Locations.Add(newLocation);
            await _appDbContext.SaveChangesAsync();
            var locationDto = new LocationReadDTO
            {
                Id = newLocation.Id,
                City = newLocation.City,
                PostCode = newLocation.PostCode,
                Street = newLocation.Street,
                Country = newLocation.Country
            };

            return CreatedAtAction(nameof(GetLocations), new { id = newLocation.Id }, locationDto);




        }
        


        
    }
}
