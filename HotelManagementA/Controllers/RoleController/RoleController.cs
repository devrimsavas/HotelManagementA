using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementA.Models.StaffModels;
using HotelManagementA.DTOs.RoleDTOs;
using HotelManagementA.Models;


namespace HotelManagementA.Controllers.RoleController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RoleController:ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        
        public RoleController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //GET ALL ROLES 
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<RoleReadDTO>>> GetRoles()
        {
            var roles = await _appDbContext.Roles
                .Select(r => new RoleReadDTO
                {
                    Id = r.Id,
                    Type = r.Type,
                }

                )
                .ToListAsync();
            return Ok(roles);

        }
        [HttpPost]
        public async Task<ActionResult<RoleReadDTO>> AddRole([FromBody] RoleCreateDTO newRoleDTO)
        {
            if (string.IsNullOrWhiteSpace(newRoleDTO.Type))
                return BadRequest(new { Message = "Role Type cannot be empty" });

            var normalized = newRoleDTO.Type.Trim().ToLower();

            var existedRole = await _appDbContext.Roles
                .FirstOrDefaultAsync(r => r.Type.ToLower() == normalized);

            if (existedRole != null)
                return Conflict(new { Message = "This role already exists" });

            var newType = new Role
            {
                Type = newRoleDTO.Type.Trim()
            };

            _appDbContext.Roles.Add(newType);
            await _appDbContext.SaveChangesAsync();

            var result = new RoleReadDTO
            {
                Id = newType.Id,
                Type = newType.Type
            };

            return CreatedAtAction(nameof(GetRoles), new { id = newType.Id }, result);
        }


        //update 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleCreateDTO roleCreateDTO)
        {
            var existedRole = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (existedRole==null) 
            {
                return BadRequest(new { Message = "Role Id does not exist" });
                
            }
            //update is empty 
            if (string.IsNullOrWhiteSpace(roleCreateDTO.Type))
            {
                return BadRequest(new { Message = "Role type cannot be empty" });
            }

            // update 
            existedRole.Type = roleCreateDTO.Type; 

            await _appDbContext.SaveChangesAsync();
            return Ok(new { Message = "Role type is updated" });

        }

        //delete 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var existedRole = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (existedRole == null)
            {
                return NotFound(new { Message = "Role Id does not exist" });

            }

            //delete 
            _appDbContext.Roles.Remove(existedRole);
            await _appDbContext.SaveChangesAsync();
            return Ok(new { Message = "Role type is deleted" });

        }


            
        
            
    }
}
