using Microsoft.AspNetCore.Mvc;
using SkyworksTest.Models;

namespace SkyworksTest.Controllers;

[ApiController]
[Route("api/School")]
public class SchoolController : ControllerBase
{
    private readonly SkyworksDbContext _context;

    public SchoolController(SkyworksDbContext context) {
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        var schoolList = _context.Schools.Select(school => new {Id = school.Id, Name =school.Name}).ToList();

        if (!schoolList.Any()) {
            return NotFound(new {
                    Status = StatusCodes.Status404NotFound,
                    Error = "No schools found"
                });
        }

        return Ok(schoolList);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Show(int id)
    {
        // var school = _context.Schools.Find(id);
        var school = _context.Schools.Where(school => school.Id == id)
            .Select(school => new {
                Id = school.Id,
                Name = school.Name,
                Principal = school.PrincipalName,
                Groups = school.Groups.Select(group => new {
                    Id = group.Id,
                    Name = group.Name,
                    Employee = group.EmployeeId
                })
                .ToList(),
                Employees = school.Employees.Select(employee => new {
                    Id = employee.Id,
                    Name = String.Concat(employee.FirstName, " ", employee.PaternalSurname, " ", employee.MaternalSurname)
                })
                .ToList()
            })
            .FirstOrDefault();

        if (school == null) {
            return NotFound(new {
                Status = StatusCodes.Status404NotFound,
                Error = "No School Found"
            });
        }

        return Ok(school);
    }

    [HttpGet]
    [Route("{id}/Group")]
    public IActionResult Groups(int id) {
        var groups = _context.Groups.Select(group => new {
            Id = group.Id,
            Name = group.Name,
            EmployeeId = group.EmployeeId,
            SchoolId = group.SchoolId
        })
        .Where(group => group.SchoolId == id)
        .ToList();

        return Ok(groups);
    }

    [HttpGet]
    [Route("{id}/Employee")]
    public IActionResult Employees(int id) {
        var employees = _context.Employees.Select(employee => new {
            Id = employee.Id,
            Name = String.Concat(employee.FirstName, employee.PaternalSurname, employee.MaternalSurname),
            SchoolId = employee.SchoolId
        })
        .Where(employee => employee.SchoolId == id)
        .ToList();

        return Ok(employees);
    }
}