using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models.AuthModels;
using HotelManagementA.DTOs.AuthDTOs;
using HotelManagementA.Models;
using HotelManagementA.Models.StaffModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementA.Services
{
    public class AuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthService(AppDbContext appDbContext, JwtSettings jwtSettings)
        {
            _appDbContext = appDbContext;
            _jwtSettings = jwtSettings;
        }

        // user verification 
        public async Task<bool> ValidateEmployeeAsync(LoginRequestDTO loginRequest)
        {
            // existed user 
            var employee = await _appDbContext.Employees
                .Include(e => e.Role)
                .SingleOrDefaultAsync(e => e.Email == loginRequest.Email);

            if (employee == null)
                return false;

            // password hash (prevent null hash crash)
            if (string.IsNullOrWhiteSpace(employee.PasswordHash))
                return false;

            var hasher = new PasswordHasher<Employee>();
            var result = hasher.VerifyHashedPassword(
                employee,
                employee.PasswordHash,
                loginRequest.Password
            );

            return result == PasswordVerificationResult.Success;
        }

        // get user for token 
        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _appDbContext.Employees
                .Include(e => e.Role)
                .SingleOrDefaultAsync(e => e.Email == email);
        }

        // Create Token 
        public string GenerateToken(Employee employee)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, employee.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, employee.Role?.Type ?? "Employee"),
                new Claim("EmployeeId", employee.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // token 
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
