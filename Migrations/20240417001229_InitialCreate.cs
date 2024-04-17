using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UescCoursesAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Syllabus = table.Column<string>(type: "TEXT", nullable: true),
                    Workload = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.DisciplineId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Rules = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PedagogicProjects",
                columns: table => new
                {
                    PedagogicProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedagogicProjects", x => x.PedagogicProjectId);
                    table.ForeignKey(
                        name: "FK_PedagogicProjects_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curriculums",
                columns: table => new
                {
                    CurriculumId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Workload = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    PedagogicProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculums", x => x.CurriculumId);
                    table.ForeignKey(
                        name: "FK_Curriculums_PedagogicProjects_PedagogicProjectId",
                        column: x => x.PedagogicProjectId,
                        principalTable: "PedagogicProjects",
                        principalColumn: "PedagogicProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumDiscipline",
                columns: table => new
                {
                    CurriculumsCurriculumId = table.Column<int>(type: "INTEGER", nullable: false),
                    DisciplinesDisciplineId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumDiscipline", x => new { x.CurriculumsCurriculumId, x.DisciplinesDisciplineId });
                    table.ForeignKey(
                        name: "FK_CurriculumDiscipline_Curriculums_CurriculumsCurriculumId",
                        column: x => x.CurriculumsCurriculumId,
                        principalTable: "Curriculums",
                        principalColumn: "CurriculumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumDiscipline_Disciplines_DisciplinesDisciplineId",
                        column: x => x.DisciplinesDisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Name", "Status" },
                values: new object[] { 1, "Ciência da Computação", "Ativo" });

            migrationBuilder.InsertData(
                table: "Disciplines",
                columns: new[] { "DisciplineId", "Syllabus", "Title", "Workload" },
                values: new object[] { 1, "Ferramentas e técnicas para o desenvolvimento de aplicações Web", "Programação Web", 60 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "Password", "Rules" },
                values: new object[] { 1, "admin", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "PedagogicProjects",
                columns: new[] { "PedagogicProjectId", "CourseId", "Status", "Year" },
                values: new object[] { 1, 1, "Ativo", 2004 });

            migrationBuilder.InsertData(
                table: "Curriculums",
                columns: new[] { "CurriculumId", "PedagogicProjectId", "Status", "Workload", "Year" },
                values: new object[] { 1, 1, "Ativo", 0, 2004 });

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumDiscipline_DisciplinesDisciplineId",
                table: "CurriculumDiscipline",
                column: "DisciplinesDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_PedagogicProjectId",
                table: "Curriculums",
                column: "PedagogicProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedagogicProjects_CourseId",
                table: "PedagogicProjects",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculumDiscipline");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Curriculums");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "PedagogicProjects");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
