using System.ComponentModel.DataAnnotations;
using SkyworksTest.Helpers.Enums;

namespace SkyworksTest.Models;

public class Student
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string PaternalSurname { get; set; } = String.Empty;
    [MaxLength(50)]
    public string ?MaternalSurname { get; set; } = String.Empty;
    public StudentStatus Status { get; set; } = StudentStatus.Cursando;
    [Phone]
    [MaxLength(12)]
    public string Phone { get; set; } = String.Empty;

    public int ?GroupId { get; set; }
    public Group ?Group { get; set; }

    public string GetStatusName()
    {
        return Enum.GetName(typeof(StudentStatus), Status);
    }
}