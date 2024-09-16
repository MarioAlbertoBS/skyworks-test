using System.Linq.Expressions;
using SkyworksTest.Models;

namespace SkyworksTest.Helpers;

public class Seeder
{
    public static void Seed(SkyworksDbContext context) {
        // Create School Data
        School[] schools = [
            new School {Name = "Escuela 1", PrincipalName = "Alvaro Ramirez"},
            new School {Name = "Escuela 2", PrincipalName = "Rogelio Sainz"},
            new School {Name = "Escuela 3", PrincipalName = "Priscila Jimenez"}
        ];

        if (!context.Schools.Any()) {
            context.Schools.AddRange(schools);

            context.SaveChanges();

            // Create groups for the new schools
            Group[] groups = [
                new Group {Name = "A", SchoolId = schools[0].Id},
                new Group {Name = "B", SchoolId = schools[0].Id},
                new Group {Name = "C", SchoolId = schools[1].Id},
                new Group {Name = "D", SchoolId = schools[1].Id},
                new Group {Name = "E", SchoolId = schools[2].Id},
                new Group {Name = "F", SchoolId = schools[2].Id}
            ];

            context.Groups.AddRange(groups);
            context.SaveChanges();
        }
    }
}