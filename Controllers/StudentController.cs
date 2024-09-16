using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SkyworksTest.Helpers.Dtos;
using SkyworksTest.Helpers.Filters;
using SkyworksTest.Models;

namespace SkyworksTest.Controllers;

[ApiController]
[Route("api/Student")]
public class StudentController : ControllerBase
{
    private readonly SkyworksDbContext _context;

    public StudentController(SkyworksDbContext context) {
        _context = context;
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Show(int id) {
        var student = _context.Students.Where(student => student.Id == id)
            .Select(student => new {
                Id = student.Id,
                Name = student.FirstName,
                PaternalSurname = student.PaternalSurname,
                MaternalSurname = student.MaternalSurname,
                Phone = student.Phone,
                Status = student.Status,
                StatusText = student.GetStatusName()
            })
            .FirstOrDefault();

        if (student == null) {
            return NotFound(new {
                Status = StatusCodes.Status404NotFound,
                Error = "Student Not Found"
            });
        }

        return Ok(student);
    }

    [HttpPost]
    [Route("")]
    public IActionResult Register([FromBody] AddStudentRequestDto request) {
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

        // Validate the group exists
        bool group = _context.Groups.Any(group => group.Id == request.GroupId);
        if (!group) {
            return BadRequest(new { 
                Status = StatusCodes.Status400BadRequest,
                Field = "GroupId",
                Error = "The Group does not exists."
             });
        }

        // Create student
        Student student = new Student {
            FirstName = request.Name,
            PaternalSurname = request.PaternalSurname,
            MaternalSurname = request.MaternalSurname,
            Phone = request.Phone,
            GroupId = request.GroupId

        };

        // Store in database
        _context.Students.Add(student);
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
            Id = student.Id,
            Name = student.FirstName,
            PaternalSurname = student.PaternalSurname,
            MaternalSurname = student.MaternalSurname,
            Phone = student.Phone,
        });
    }

    [HttpPost]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateStudentRequestDto request) {
        // Validate model
        if (!ModelState.IsValid) {
            var errors = ModelState.GetValidationState;
            var mdState = ModelState;
            foreach (KeyValuePair<string, ModelStateEntry> entry in ModelState) {
                var key = entry.Key;
                var error = entry.Value.Errors.FirstOrDefault();
                var errorList = entry.Value.Errors;

                return BadRequest(new {
                    Status = StatusCodes.Status400BadRequest,
                    Field = entry.Key,
                    Error = entry.Value.Errors.FirstOrDefault()?.ErrorMessage
                });
            }
        }

        // Validate the student exists
        var student = _context.Students.Find(id);
        if (student == null) {
            return BadRequest(new { 
                Status = StatusCodes.Status404NotFound,
                Field = "Id",
                Error = "The user does not exists."
             });
        }

        student.FirstName = request.Name;
        student.PaternalSurname = request.PaternalSurname;
        student.MaternalSurname = request.MaternalSurname;
        student.Phone = request.Phone;
        student.Status = request.Status;

        try {
            _context.SaveChanges();
        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, new {
                Status = StatusCodes.Status500InternalServerError,
                Error = "Internal Error"
            });
        }

        return Ok(new {
            Id = student.Id,
            Name = student.FirstName,
            PaternalSurname = student.PaternalSurname,
            MaternalSurname = student.MaternalSurname,
            Status = student.Status
        });
    }
}