using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models;
using HotelManagementA.Models.StaffModels;
using HotelManagementA.Models.HotelModels;
using HotelManagementA.Models.AuthModels;
using HotelManagementA.DTOs;
using HotelManagementA.Services;
using HotelManagementA.DTOs.HoteDTOs;
using HotelManagementA.DTOs.RoomDTOs;
using HotelManagementA.DTOs.StaffDTOs;


namespace HotelManagementA.Controllers.StaffController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StaffController:ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public StaffController (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //all employees in all hotels 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDTO>>> GetEmployees()
        {
            var employees = await _appDbContext.Employees
                .Include(e => e.Role)
                .Include(e => e.Hotel)
                .Select(e => new EmployeeReadDTO
                {
                    Id = e.Id,
                    FullName = e.FirstName + " " + e.Surname,
                    Email = e.Email,
                    Role = e.Role != null ? e.Role.Type : "Unknown",
                    HotelName = e.Hotel != null ? e.Hotel.Name : "Unknown",
                })
                .ToListAsync();

            return Ok(employees);
        }


    }
}
