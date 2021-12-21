using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructure.Migrations
{
    public partial class TestWordToVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ConfirmedEmail = table.Column<bool>(type: "bit", nullable: false),
                    TokenPasswordRecovery = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TokenEmailConfirmation = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestsOptionWordToVideo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfQuestions = table.Column<int>(type: "int", nullable: false),
                    IsErrorTest = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsOptionWordToVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestOptionWordToVideo_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsOptionWordToVideo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordToGuess = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PossibleAnswer0 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PossibleAnswer1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PossibleAnswer2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PossibleAnswer3 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsOptionWordToVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Test",
                        column: x => x.TestId,
                        principalTable: "TestsOptionWordToVideo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOptionWordToVideo_TestId",
                table: "QuestionsOptionWordToVideo",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestsOptionWordToVideo_UserId",
                table: "TestsOptionWordToVideo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsOptionWordToVideo");

            migrationBuilder.DropTable(
                name: "TestsOptionWordToVideo");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
