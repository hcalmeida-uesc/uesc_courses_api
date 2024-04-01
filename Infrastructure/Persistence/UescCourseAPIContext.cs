using Microsoft.EntityFrameworkCore;
using UescCoursesAPI.Domain;

namespace UescCoursesAPI.Infrastructure.Persistence;

public class UescCourseAPIContext : DbContext
{
      public UescCourseAPIContext(DbContextOptions<UescCourseAPIContext> options) : base(options)
      {
         Database.EnsureCreated();
      }
   
      public DbSet<Course> Courses { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
            throw new System.NotImplementedException();
      }
}
