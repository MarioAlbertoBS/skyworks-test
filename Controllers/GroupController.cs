using Microsoft.AspNetCore.Mvc;
using SkyworksTest.Models;

namespace SkyworksTest.Controllers;

[ApiController]
[Route("api/Group")]
public class GroupController : ControllerBase
{
    private readonly SkyworksDbContext _context;

    public GroupController(SkyworksDbContext context) {
        _context = context;
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Show(int id) {
        var group = _context.Groups.Where(group => group.Id == id)
            .Select(group => new {
                Id = group.Id,
                Name = group.Name,
                Employee = group.EmployeeId,
                Students = group.Students.Select(student => new {
                    Id = student.Id,
                    Name = String.Concat(student.FirstName, " ", student.PaternalSurname, " ", student.MaternalSurname),
                    Phone = student.Phone,
                    Status = student.GetStatusName()
                })
            })
            .FirstOrDefault();

        if (group == null) {
            return NotFound(new {
                Status = StatusCodes.Status404NotFound,
                Error = "No group is found."
            });
        }

        return Ok(group);
    }
}