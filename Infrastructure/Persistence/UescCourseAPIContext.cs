using Microsoft.EntityFrameworkCore;
using UescCoursesAPI.Domain;

namespace UescCoursesAPI.Infrastructure.Persistence;

public class UescCourseAPIContext : DbContext
{
      public UescCourseAPIContext(DbContextOptions<UescCourseAPIContext> options) : base(options)
      {
         //Database.EnsureDeleted();
         Database.EnsureCreated();
      }


    public DbSet<Course> Courses { get; set; }
    public DbSet<PedagogicProject> PedagogicProjects { get; set; }
    public DbSet<Curriculum> Curriculums { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);

            modelBuilder.Entity<PedagogicProject>().ToTable("PedagogicProjects");
            modelBuilder.Entity<PedagogicProject>().HasKey(p => p.PedagogicProjectId);
            modelBuilder.Entity<PedagogicProject>().HasOne(p => p.Course).WithMany(c => c.PedagogicProjects).HasForeignKey(p => p.CourseId);

            modelBuilder.Entity<Curriculum>().ToTable("Curriculums");
            modelBuilder.Entity<Curriculum>().HasKey(c => c.CurriculumId);
            modelBuilder.Entity<Curriculum>().HasOne(c => c.PedagogicProject).WithOne(p => p.Curriculum).HasForeignKey<Curriculum>(c => c.PedagogicProjectId);

            modelBuilder.Entity<Discipline>().ToTable("Disciplines");
            modelBuilder.Entity<Discipline>().HasKey(d => d.DisciplineId);
            modelBuilder.Entity<Discipline>().HasMany(d => d.Curriculums).WithMany(c => c.Disciplines);
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
            #region Mysql ConnectionString
            // var stringconnection = "server=localhost;database=uesc_courses;user=uesc_courses;password=colcicuesc";
            // var serverVersion = ServerVersion.AutoDetect(stringconnection);
            // optionsBuilder.UseMySql(stringconnection,serverVersion)
            //       .EnableSensitiveDataLogging()
            //       .EnableDetailedErrors();
            #endregion

            #region SqLite ConnectionString
            var stringconnection = "Data Source=uesc_courses.db";
            optionsBuilder.UseSqlite(stringconnection)
                  .EnableSensitiveDataLogging()
                  .EnableDetailedErrors();
            #endregion
      }
}
