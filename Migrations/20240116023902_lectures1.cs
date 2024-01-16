using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students_Management_Api.Migrations
{
    /// <inheritdoc />
    public partial class lectures1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
        name: "lecturestudent");

            migrationBuilder.CreateTable(
    name: "LectureStudent",
    columns: table => new
    {
        LecturesId = table.Column<int>(type: "int", nullable: false),
        StudentsId = table.Column<int>(type: "int", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_LectureStudent", x => new { x.LecturesId, x.StudentsId });
        table.ForeignKey(
            name: "FK_LectureStudent_Lecture_LecturesId",
            column: x => x.LecturesId,
            principalTable: "Lecture",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        table.ForeignKey(
            name: "FK_LectureStudent_Student_StudentsId",
            column: x => x.StudentsId,
            principalTable: "Student",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    })
    .Annotation("MySQL:Charset", "utf8mb4");


            migrationBuilder.CreateIndex(
                name: "IX_LectureStudent_StudentsId",
                table: "LectureStudent",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LectureStudent_StudentsId",
                table: "LectureStudent");

            migrationBuilder.CreateIndex(
                name: "IX_LectureStudent_StudentsId",
                table: "LectureStudent",
                column: "StudentsId");
        }
    }
}
