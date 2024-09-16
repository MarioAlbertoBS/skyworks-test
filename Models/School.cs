using System.ComponentModel.DataAnnotations;

namespace SkyworksTest.Models;

public class School
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;
    [MaxLength(150)]
    public string PrincipalName { get; set; } = String.Empty;
    
    public IEnumerable<Student> ?Students { get; set; }
    public IEnumerable<Employee> ?Employees { get; set; }
    public IEnumerable<Group> ?Groups { get; set; }
}