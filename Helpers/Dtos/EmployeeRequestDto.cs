using Microsoft.AspNetCore.Mvc;
using SkyworksTest.Helpers.Filters;

namespace SkyworksTest.Helpers.Dtos;

public class AddEmployeeRequestDto
{
    public string Name { get; set; }
    public string PaternalSurname { get; set; }
    public string MaternalSurname { get; set; }
    public int SchoolId { get; set; }
    public int GroupId { get; set; }
}