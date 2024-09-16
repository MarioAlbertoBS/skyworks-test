using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SkyworksTest.Helpers.Enums;
using SkyworksTest.Helpers.Filters;

namespace SkyworksTest.Helpers.Dtos;

public class AddStudentRequestDto
{
    public string Name { get; set; }
    public string PaternalSurname { get; set; }
    public string MaternalSurname { get; set; }
    public string Phone { get; set; }
    public int SchoolId { get; set; }
    public int GroupId { get; set; }
}

public class UpdateStudentRequestDto
{
    public string Name { get; set; }
    public string PaternalSurname { get; set; }
    public string MaternalSurname { get; set; }
    public string Phone { get; set; }
    public StudentStatus Status { get; set; }
}