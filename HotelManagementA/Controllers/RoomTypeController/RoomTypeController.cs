using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models;
using HotelManagementA.Models.HotelModels;
using HotelManagementA.DTOs;
using HotelManagementA.DTOs.HoteDTOs;
using HotelManagementA.DTOs.RoomDTOs;
using Microsoft.AspNetCore.Mvc;
using HotelManagementA.DTOs.RoomTypeDTOs;


namespace HotelManagementA.Controllers.RoomTypeController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RoomTypeController:ControllerBase 
    {
        private readonly AppDbContext _appDbContext;
        public RoomTypeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeReadDTO>>> GetRoomTypes()
        {
            var roomTypes = await _appDbContext.RoomTypes
                .Select(t => new RoomTypeReadDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Capacity = t.Capacity,
                    Price = t.Price,
                })
                .ToListAsync();
            return Ok(roomTypes);

        }
        [HttpPost]
        public async Task<ActionResult<RoomTypeReadDTO>> AddRoomType([FromBody] RoomTypeCreateDTO roomTypeDTO)
        {
            if (string.IsNullOrWhiteSpace(roomTypeDTO.Name))
            {
                return BadRequest(new { Message = "Room type name is required" });
            }

            var roomType = new RoomType
            {
                Name = roomTypeDTO.Name,
                Description = roomTypeDTO.Description,
                Price = roomTypeDTO.Price,
                Capacity = roomTypeDTO.Capacity,
            };
            _appDbContext.RoomTypes.Add(roomType);
            await _appDbContext.SaveChangesAsync();
            //results
            var result = new RoomTypeReadDTO
            {
                Id = roomType.Id,
                Name = roomType.Name,
                Description = roomType.Description,
                Price = roomType.Price,
                Capacity = roomType.Capacity,
            };

            return CreatedAtAction(nameof(GetRoomTypes), new { id = roomType.Id }, result);

        }


            



    }
}
