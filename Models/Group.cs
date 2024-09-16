using System.ComponentModel.DataAnnotations;

namespace SkyworksTest.Models;

public class Group
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;

    public int SchoolId { get; set; }
    public School ?School { get; set; }

    public int ?EmployeeId { get; set; }
    public Employee ?Employee { get; set; }

    public IEnumerable<Student> ?Students { get; set; }
}