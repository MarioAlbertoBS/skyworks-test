using Microsoft.EntityFrameworkCore;
using SkyworksTest.Helpers.Enums;

namespace SkyworksTest.Models;

public class SkyworksDbContext : DbContext
{
    public DbSet<School> Schools { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Student> Students { get; set; }

    public SkyworksDbContext(DbContextOptions<SkyworksDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Set foreign keys
        // Student belongs to a group
        modelBuilder.Entity<Student>()
            .HasOne(student => student.Group)
            .WithMany(group => group.Students)
            .HasForeignKey(student => student.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // Employee belongs to a School
        modelBuilder.Entity<Employee>()
            .HasOne(employee => employee.School)
            .WithMany(school => school.Employees)
            .HasForeignKey(employee => employee.SchoolId)
            .OnDelete(DeleteBehavior.Cascade);

        // A group belongs to a School
        modelBuilder.Entity<Group>()
            .HasOne(group => group.School)
            .WithMany(school => school.Groups)
            .HasForeignKey(group => group.SchoolId)
            .OnDelete(DeleteBehavior.NoAction);

        // A group can have a employee assigned
        modelBuilder.Entity<Group>()
            .HasOne(group => group.Employee)
            .WithOne(employee => employee.Group)
            .OnDelete(DeleteBehavior.Cascade);

        // Set constraints
        // Limit Status to only Enum values
        modelBuilder.Entity<Student>()
            .Property(student => student.Status)
            .HasConversion(value => value.ToString(), value => (StudentStatus)Enum.Parse(typeof(StudentStatus), value))
            .HasMaxLength(50);
    }
}