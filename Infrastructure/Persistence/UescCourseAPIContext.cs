using Microsoft.EntityFrameworkCore;
using UescCoursesAPI.Domain;

namespace UescCoursesAPI.Infrastructure.Persistence;

public class UescCourseAPIContext : DbContext
{
      public UescCourseAPIContext(DbContextOptions<UescCourseAPIContext> options) : base(options)
      {
         //Database.EnsureCreated();
      }


    public DbSet<Course> Courses { get; set; }
    public DbSet<PedagogicProject> PedagogicProjects { get; set; }
    public DbSet<Curriculum> Curriculums { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<User> Users { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
            modelBuilder.Entity<Course>().HasData(
                  new Course { CourseId = 1, Name = "Ciência da Computação", Status = "Ativo" }
            );

            modelBuilder.Entity<PedagogicProject>().ToTable("PedagogicProjects");
            modelBuilder.Entity<PedagogicProject>().HasKey(p => p.PedagogicProjectId);
            modelBuilder.Entity<PedagogicProject>().HasOne(p => p.Course).WithMany(c => c.PedagogicProjects).HasForeignKey(p => p.CourseId);
            modelBuilder.Entity<PedagogicProject>().HasData(
                  new PedagogicProject { PedagogicProjectId = 1, CourseId = 1, Year = 2004, Status = "Ativo" }
            );

            modelBuilder.Entity<Curriculum>().ToTable("Curriculums");
            modelBuilder.Entity<Curriculum>().HasKey(c => c.CurriculumId);
            modelBuilder.Entity<Curriculum>().HasOne(c => c.PedagogicProject).WithOne(p => p.Curriculum).HasForeignKey<Curriculum>(c => c.PedagogicProjectId);
            modelBuilder.Entity<Curriculum>().HasData(
                  new Curriculum { CurriculumId = 1, PedagogicProjectId = 1, Year = 2004, Status = "Ativo" }
            );

            modelBuilder.Entity<Discipline>().ToTable("Disciplines");
            modelBuilder.Entity<Discipline>().HasKey(d => d.DisciplineId);
            modelBuilder.Entity<Discipline>().HasMany(d => d.Curriculums).WithMany(c => c.Disciplines);
            modelBuilder.Entity<Discipline>().HasData(
                  new Discipline
                  { 
                        DisciplineId = 1,
                        Title = "Programação Web",
                        Syllabus = "Ferramentas e técnicas para o desenvolvimento de aplicações Web",
                        Workload = 60
                  }
            );

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().HasData(
                  new User { UserId = 1, Login = "admin", Password = "admin", Rules = "admin" }
            );
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
