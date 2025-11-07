using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models;
using HotelManagementA.Models.HotelModels;
using HotelManagementA.DTOs;
using HotelManagementA.DTOs.HoteDTOs;
using HotelManagementA.DTOs.RoomDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementA.Controllers.RoomController

{
    [ApiController]
    [Route("api/[Controller]")]

    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public RoomController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("{hotelId}")]
        public async Task<ActionResult<IEnumerable<RoomReadDTO>>> GetAHotelRooms(int hotelId)
        {
            var existedHotel=await _appDbContext.Hotels.FirstOrDefaultAsync(h=>h.Id==hotelId);
            if (existedHotel == null)
            {
                return BadRequest(new { Message = "Requested Hotel does not exist" });
            }
            var rooms = await _appDbContext.Rooms
                .Where(r => r.HotelId == existedHotel.Id)
                .Include(r => r.RoomType)
                .Select(r => new RoomReadDTO
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType.Name,
                    HotelId = r.HotelId,
                    IsCleaned = r.IsCleaned

                })
                .ToListAsync();
            return Ok(rooms);


        }
    }
}
