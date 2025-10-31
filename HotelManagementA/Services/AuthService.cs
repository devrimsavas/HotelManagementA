using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models.AuthModels;
using HotelManagementA.Models;
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

        
    }
}
