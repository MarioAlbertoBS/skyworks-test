using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SkyworksTest.Helpers.Dtos;
using SkyworksTest.Models;

namespace SkyworksTest.Controllers;

[ApiController]
[Route("api/Employee")]
public class EmployeeController : ControllerBase
{
    private readonly SkyworksDbContext _context;

    public EmployeeController(SkyworksDbContext context) {
        _context = context;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Register([FromBody] AddEmployeeRequestDto request) {
        // Validate model
        if (!ModelState.IsValid) {
            var errors = ModelState.GetValidationState;
            var mdState = ModelState;
            foreach (KeyValuePair<string, ModelStateEntry> entry in ModelState) {
                var key = entry.Key;
                var error = entry.Value.Errors.FirstOrDefault();

                return BadRequest(new {
                    Status = StatusCodes.Status400BadRequest,
                    Field = entry.Key,
                    Error = entry.Value.Errors.FirstOrDefault()?.ErrorMessage
                });
            }
        }

        // Validate the school exists
        bool school = _context.Schools.Any(school => school.Id == request.SchoolId);
        if (!school) {
            return BadRequest(new { 
                Status = StatusCodes.Status400BadRequest,
                Field = "SchoolId",
                Error = "The School does not exists."
             });
        }

        // Validate the group exists
        var group = _context.Groups.Find(request.GroupId);
        if (group == null) {
            return BadRequest(new { 
                Status = StatusCodes.Status400BadRequest,
                Field = "GroupId",
                Error = "The Group does not exists."
             });
        }

        Employee employee = new Employee {
            FirstName = request.Name,
            PaternalSurname = request.PaternalSurname,
            MaternalSurname = request.MaternalSurname,
            SchoolId = request.SchoolId,
        };

        // Relate the group with the employee
        employee.Group = group;

        _context.Employees.Add(employee);
        
        try {
            _context.SaveChanges();
        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, new {
                Status = StatusCodes.Status500InternalServerError,
                Error = "Internal Error"
            });
        }
        
        return CreatedAtAction(nameof(Register), new {
            Status = StatusCodes.Status201Created,
            Id = employee.Id,
            Name = employee.FirstName,
            PaternalSurname = employee.PaternalSurname,
            MaternalSurname = employee.MaternalSurname,
            SchoolId = employee.SchoolId,
        });
    }
}