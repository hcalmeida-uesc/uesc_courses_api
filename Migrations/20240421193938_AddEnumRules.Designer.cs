﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UescCoursesAPI.Infrastructure.Persistence;

#nullable disable

namespace UescCoursesAPI.Migrations
{
    [DbContext(typeof(UescCourseAPIContext))]
    [Migration("20240421193938_AddEnumRules")]
    partial class AddEnumRules
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("CurriculumDiscipline", b =>
                {
                    b.Property<int>("CurriculumsCurriculumId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DisciplinesDisciplineId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CurriculumsCurriculumId", "DisciplinesDisciplineId");

                    b.HasIndex("DisciplinesDisciplineId");

                    b.ToTable("CurriculumDiscipline");
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CourseId");

                    b.ToTable("Courses", (string)null);

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            Name = "Ciência da Computação",
                            Status = "Ativo"
                        });
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.Curriculum", b =>
                {
                    b.Property<int>("CurriculumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PedagogicProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int>("Workload")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("CurriculumId");

                    b.HasIndex("PedagogicProjectId")
                        .IsUnique();

                    b.ToTable("Curriculums", (string)null);

                    b.HasData(
                        new
                        {
                            CurriculumId = 1,
                            PedagogicProjectId = 1,
                            Status = "Ativo",
                            Workload = 0,
                            Year = 2004
                        });
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.Discipline", b =>
                {
                    b.Property<int>("DisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Syllabus")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Workload")
                        .HasColumnType("INTEGER");

                    b.HasKey("DisciplineId");

                    b.ToTable("Disciplines", (string)null);

                    b.HasData(
                        new
                        {
                            DisciplineId = 1,
                            Syllabus = "Ferramentas e técnicas para o desenvolvimento de aplicações Web",
                            Title = "Programação Web",
                            Workload = 60
                        });
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.PedagogicProject", b =>
                {
                    b.Property<int>("PedagogicProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("PedagogicProjectId");

                    b.HasIndex("CourseId");

                    b.ToTable("PedagogicProjects", (string)null);

                    b.HasData(
                        new
                        {
                            PedagogicProjectId = 1,
                            CourseId = 1,
                            Status = "Ativo",
                            Year = 2004
                        });
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rules")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Login = "admin",
                            Password = "admin",
                            Rules = 1
                        });
                });

            modelBuilder.Entity("CurriculumDiscipline", b =>
                {
                    b.HasOne("UescCoursesAPI.Domain.Curriculum", null)
                        .WithMany()
                        .HasForeignKey("CurriculumsCurriculumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UescCoursesAPI.Domain.Discipline", null)
                        .WithMany()
                        .HasForeignKey("DisciplinesDisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.Curriculum", b =>
                {
                    b.HasOne("UescCoursesAPI.Domain.PedagogicProject", "PedagogicProject")
                        .WithOne("Curriculum")
                        .HasForeignKey("UescCoursesAPI.Domain.Curriculum", "PedagogicProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PedagogicProject");
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.PedagogicProject", b =>
                {
                    b.HasOne("UescCoursesAPI.Domain.Course", "Course")
                        .WithMany("PedagogicProjects")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.Course", b =>
                {
                    b.Navigation("PedagogicProjects");
                });

            modelBuilder.Entity("UescCoursesAPI.Domain.PedagogicProject", b =>
                {
                    b.Navigation("Curriculum");
                });
#pragma warning restore 612, 618
        }
    }
}