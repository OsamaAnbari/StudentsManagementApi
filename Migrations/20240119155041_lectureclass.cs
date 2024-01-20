using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Students_Management_Api.Migrations
{
    /// <inheritdoc />
    public partial class lectureclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Class_Teacher_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecture_Teacher_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "longtext", nullable: true),
                    Body = table.Column<string>(type: "longtext", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMessages_Student_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentMessages_Teacher_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeacherMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "longtext", nullable: true),
                    Body = table.Column<string>(type: "longtext", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherMessage_Teacher_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClassStudent",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "int", nullable: false),
                    ClassesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStudent", x => new { x.ClassesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ClassStudent_Class_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassStudent_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LectureStudent",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "int", nullable: false),
                    LecturesId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "StudentTeacherMessage",
                columns: table => new
                {
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    ReceivedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTeacherMessage", x => new { x.ReceivedId, x.ReceiverId });
                    table.ForeignKey(
                        name: "FK_StudentTeacherMessage_Student_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTeacherMessage_TeacherMessage_ReceivedId",
                        column: x => x.ReceivedId,
                        principalTable: "TeacherMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Class_TeacherID",
                table: "Class",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudent_StudentsId",
                table: "ClassStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_TeacherID",
                table: "Lecture",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_LectureStudent_StudentsId",
                table: "LectureStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMessages_ReceiverId",
                table: "StudentMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMessages_SenderId",
                table: "StudentMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacherMessage_ReceiverId",
                table: "StudentTeacherMessage",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherMessage_SenderId",
                table: "TeacherMessage",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassStudent");

            migrationBuilder.DropTable(
                name: "LectureStudent");

            migrationBuilder.DropTable(
                name: "StudentMessages");

            migrationBuilder.DropTable(
                name: "StudentTeacherMessage");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropTable(
                name: "TeacherMessage");
        }
    }
}
