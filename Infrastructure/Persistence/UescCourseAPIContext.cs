using Microsoft.EntityFrameworkCore;
using UescCoursesAPI.Domain;

namespace UescCoursesAPI.Infrastructure.Persistence;

public class UescCourseAPIContext : DbContext
{
      public UescCourseAPIContext(DbContextOptions<UescCourseAPIContext> options) : base(options)
      {
         Database.EnsureCreated();
      }

    public UescCourseAPIContext()
    {
    }

    public DbSet<Course> Courses { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
            var stringconnection = "server=localhost;database=uesc_courses;user=uesc_courses;password=colcicuesc";
            var serverVersion = ServerVersion.AutoDetect(stringconnection);
            optionsBuilder.UseMySql(stringconnection,serverVersion)
                  .EnableSensitiveDataLogging()
                  .EnableDetailedErrors();
      }
}
