using System.ComponentModel.DataAnnotations;

namespace SkyworksTest.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string PaternalSurname { get; set; } = String.Empty;
    [MaxLength(50)]
    public string ?MaternalSurname  { get; set; } = String.Empty;
    
    public int SchoolId { get; set; }
    public School ?School { get; set; }
    
    public Group ?Group { get; set; }
}