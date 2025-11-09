using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models;
using HotelManagementA.Models.HotelModels;
using HotelManagementA.DTOs;
using HotelManagementA.DTOs.HoteDTOs;
using HotelManagementA.DTOs.RoomDTOs;
using HotelManagementA.DTOs.RoomTypeDTOs;
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
        [HttpPost("{hotelId}")]
        public async Task<ActionResult<RoomReadDTO>> AddRoom([FromBody] RoomCreateDTO roomCreateDTO,int hotelId)
        {
            var existedHotel = await _appDbContext.Hotels
                .Include(h=>h.Rooms)                
                .FirstOrDefaultAsync(h => h.Id == hotelId);

            if (existedHotel == null)
            {
                return BadRequest(new { Message = "Hotel Does not exist" });
            }

            //check room type
            var roomType = await _appDbContext.RoomTypes.FindAsync(roomCreateDTO.RoomTypeId);
            if (roomType == null)
            {
                return BadRequest(new { Message = "Invalid RoomType ID " });
            }

            //check duplicate room number
            if (existedHotel.Rooms.Any(r=>r.RoomNumber == roomCreateDTO.RoomNumber))
            {
                return BadRequest(new { Message = "This room number already exists in the hotel." });
            };

            //create a new room
            var newRoom = new Room
            {
                RoomNumber = roomCreateDTO.RoomNumber,
                RoomTypeId = roomCreateDTO.RoomTypeId,
                HotelId = hotelId,
                IsCleaned = roomCreateDTO.IsCleaned
            };
            _appDbContext.Rooms.Add(newRoom);
            await _appDbContext.SaveChangesAsync();
            //map 
            var result = new RoomReadDTO
            {
                Id = newRoom.Id,
                RoomType = roomType.Name,
                HotelId = newRoom.HotelId,
                IsCleaned = newRoom.IsCleaned
            };
            return CreatedAtAction(nameof(GetAHotelRooms), new { hotelId = newRoom.HotelId }, result);

















        }


    }
}
