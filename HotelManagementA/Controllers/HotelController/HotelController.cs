using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models;
using HotelManagementA.Models.HotelModels;
using HotelManagementA.DTOs;
using HotelManagementA.Services;
using HotelManagementA.DTOs.HoteDTOs;
using HotelManagementA.DTOs.RoomDTOs;

namespace HotelManagementA.Controllers.HotelController
{
    [ApiController]
    [Route("api/[Controller]")]

    
    public class HotelController:ControllerBase 
    {
        private readonly AppDbContext _appDbContext;
        public HotelController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //get all hotels 
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<HotelReadDTO>>> GetHotels()
        {
            var hotels = await _appDbContext.Hotels
                .Include(h => h.Location)
                .Select(c => new HotelReadDTO
                {
                    Id=c.Id,
                    Name=c.Name,
                    Description=c.Description,
                    Telephone=c.Telephone,
                    City=c.Location.City,  
                    AverageRating=c.AverageRating,
                }).ToListAsync();

            return Ok(hotels);

        }
        //get an hotel details 
        [HttpGet("{id}")] 
        public async Task<ActionResult<HotelDetailsDTO>> GetHotelDetails(int id)
        {
            var hotel =await  _appDbContext.Hotels
                 .Include(h => h.Location)
                 .Include(h => h.Rooms)
                 .Include(h => h.Reviews)
                 .ThenInclude(r => r.Customer)
                 .Where(h => h.Id == id)
                 .Select(h => new HotelDetailsDTO
                 {
                     Id = h.Id,
                     Name = h.Name,
                     Description = h.Description,
                     Telephone = h.Telephone,
                     Location = h.Location.City,
                     AverageRating = h.AverageRating,
                     TotalReviews = h.TotalReviews,
                     Rooms = h.Rooms.Select(r => new RoomReadDTO
                     {
                         Id = r.Id,
                         RoomNumber = r.RoomNumber,
                         RoomType = r.RoomType.Name,
                         HotelId = r.HotelId,
                         IsCleaned = r.IsCleaned
                     }).ToList(),
                     Reviews = h.Reviews.Select(rv => new HotelReviewReadDTO
                     {
                         Id = rv.Id,
                         Rating = rv.Rating,
                         HotelName = h.Name,
                         CustomerName = rv.Customer.FirstName + " " + rv.Customer.LastName,
                         Review = rv.Review,
                         ReviewDate = rv.ReviewDate,
                     }
                     ).ToList(),

                 })
                 .FirstOrDefaultAsync();

            if (hotel == null)
                return NotFound(new { Message = "Hotel not found" });

            return Ok(hotel);

        }

        [HttpPost] 
        public async Task<ActionResult<HotelReadDTO>> AddHotel([FromBody] HotelCreateDTO hoteldto)
        {
            if (string.IsNullOrWhiteSpace(hoteldto.Name) )
            {
                return BadRequest(new { Message="Hotel Name cannot be empty"});
            }

            //check location exists 
            var locationExists = await _appDbContext.Locations.AnyAsync(l => l.Id == hoteldto.LocationId);
            if (!locationExists)
            {
                return BadRequest(new { Message = "Location not found. Add Location first" });

            }

            //new hotel 
            var newHotel = new Hotel
            {
                Name= hoteldto.Name,
                Description= hoteldto.Description,
                Telephone= hoteldto.Telephone,
                LocationId= hoteldto.LocationId,
            };
            _appDbContext.Hotels.Add(newHotel);
            await _appDbContext.SaveChangesAsync();

            //use dto for return 
            var createDto = new HotelReadDTO
            {
                Id = newHotel.Id,
                Name= newHotel.Name,
                Description= newHotel.Description,
                Telephone= newHotel.Telephone,
                City = (await _appDbContext.Locations.FindAsync(newHotel.LocationId))?.City ?? "",
                AverageRating=newHotel.AverageRating,
            };

            return CreatedAtAction(nameof(GetHotelDetails), new {id=newHotel.Id},createDto);


            


            
        }
            


        

        
    }
}
