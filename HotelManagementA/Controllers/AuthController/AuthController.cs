using Microsoft.AspNetCore.Mvc;
using HotelManagementA.Services;
using HotelManagementA.DTOs.StaffDTOs;
using HotelManagementA.Models.AuthModels;
using HotelManagementA.DTOs.AuthDTOs;

namespace HotelManagementA.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (string.IsNullOrWhiteSpace(loginRequestDTO.Email) || string.IsNullOrWhiteSpace(loginRequestDTO.Password))
            {
                return BadRequest(new { Message = "Email and Password are required" });
            }
            var ok=await _authService.ValidateEmployeeAsync(loginRequestDTO);
            if (!ok) return Unauthorized(new { Message = "Invalid Email or Password" });

            var employee=await _authService.GetEmployeeByEmailAsync(loginRequestDTO.Email);
            if (employee is null)
            {
                return Unauthorized(new { Message = "Employee not found" });

            }
            var token = _authService.GenerateToken(employee);
            return Ok(new {Token=token});  
        }


        
    }
}
