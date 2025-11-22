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
using Microsoft.AspNetCore.Identity;



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

        //POST 
        [HttpPost]
        public async Task<ActionResult<EmployeeReadDTO>> CreateNewEmployee([FromBody] EmployeeCreateDTO employeeDTO)
        {
            if (string.IsNullOrWhiteSpace(employeeDTO.FirstName) ||
                string.IsNullOrWhiteSpace(employeeDTO.LastName) ||
                string.IsNullOrWhiteSpace(employeeDTO.Email))
            {
                return BadRequest(new { Message = "First name, last name and email are required" });
            }

            var existedEmployee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == employeeDTO.Email);
            if (existedEmployee != null)
            {
                return Conflict(new { Message = $"An employee with email {employeeDTO.Email} already exists" });
            }
            //hotel id check 
            var hotelExists = await _appDbContext.Hotels.AnyAsync(h => h.Id == employeeDTO.HotelId);
            if (!hotelExists)
            {
                return BadRequest(new { Message = "Hotel does not exist" });
            }
            

            //role 
            var roleExists = await _appDbContext.Roles.AnyAsync(r=>r.Id == employeeDTO.RoleId);
            if (!roleExists) 
            {
                return BadRequest(new { Message = "Roles Does not Exists" });
            }






            //create new employee 
            var newEmployee = new Employee
            {
                FirstName = employeeDTO.FirstName,
                Surname = employeeDTO.LastName,
                Email = employeeDTO.Email,
                RoleId = employeeDTO.RoleId,
                HotelId = employeeDTO.HotelId,

            };
            //craete hasher
            var passwordHasher = new PasswordHasher<Employee>();
            //add hashedpass
            newEmployee.PasswordHash = passwordHasher.HashPassword(newEmployee, employeeDTO.Password);

            //create
            _appDbContext.Add(newEmployee);
            await _appDbContext.SaveChangesAsync();
            var result= new EmployeeReadDTO
            {
                Id = newEmployee.Id,
                FullName = newEmployee.FirstName + " " + newEmployee.Surname,
                Email = newEmployee.Email,
                Role = (await _appDbContext.Roles.FindAsync(newEmployee.RoleId))?.Type ?? "",
                HotelName = (await _appDbContext.Hotels.FindAsync(newEmployee.HotelId))?.Name ?? ""
            };

            return CreatedAtAction(nameof(GetEmployees), new { id = newEmployee.Id }, result);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeReadDTO>> UpdateEmployee(
    int id,
    [FromBody] EmployeeUpdateDTO dto)
        {
            var employee = await _appDbContext.Employees
                .Include(e => e.Role)
                .Include(e => e.Hotel)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            // Role check
            var roleExists = await _appDbContext.Roles.AnyAsync(r => r.Id == dto.RoleId);
            if (!roleExists)
                return BadRequest(new { Message = "Role does not exist" });

            // Hotel check
            var hotelExists = await _appDbContext.Hotels.AnyAsync(h => h.Id == dto.HotelId);
            if (!hotelExists)
                return BadRequest(new { Message = "Hotel does not exist" });

            // UPDATE FIELDS
            employee.FirstName = dto.FirstName;
            employee.Surname = dto.LastName;
            employee.Email = dto.Email;
            employee.RoleId = dto.RoleId;
            employee.HotelId = dto.HotelId;

            // Password update (optional)
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                var hasher = new PasswordHasher<Employee>();
                employee.PasswordHash = hasher.HashPassword(employee, dto.Password);
            }

            await _appDbContext.SaveChangesAsync();

            // RETURN DTO
            var result = new EmployeeReadDTO
            {
                Id = employee.Id,
                FullName = employee.FirstName + " " + employee.Surname,
                Email = employee.Email,
                Role = employee.Role!.Type,
                HotelName = employee.Hotel!.Name
            };

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { Message = "Employee deleted successfully" });
        }







    }
}
