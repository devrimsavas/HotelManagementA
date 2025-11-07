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
        public async Task<ActionResult<RoomTypeReadDTO>> AddRoomType([FromBody]RoomTypeReadDTO roomType)
        {
            if (string.IsNullOrWhiteSpace(roomType.Name))
            {
                return BadRequest(new { Message = "Room type name is required" });
            }

            



    }
}
