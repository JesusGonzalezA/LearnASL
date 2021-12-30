using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructure.Migrations
{
    public partial class Test_and_user : Migration
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
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsMimic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordToGuess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoHelp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsMimic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Test_QuestionMimicConfiguration",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsOptionVideoToWord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoToGuess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PossibleAnswer0 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PossibleAnswer1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PossibleAnswer2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PossibleAnswer3 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsOptionVideoToWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Test_QuestionOptionVideoToWordConfiguration",
                        column: x => x.TestId,
                        principalTable: "Tests",
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
                    UserAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsOptionWordToVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Test_QuestionOptionWordToVideoConfiguration",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsQA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordToGuess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    VideoUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsQA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Test_QuestionQaConfiguration",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsMimic_TestId",
                table: "QuestionsMimic",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOptionVideoToWord_TestId",
                table: "QuestionsOptionVideoToWord",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOptionWordToVideo_TestId",
                table: "QuestionsOptionWordToVideo",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsQA_TestId",
                table: "QuestionsQA",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_UserId",
                table: "Tests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsMimic");

            migrationBuilder.DropTable(
                name: "QuestionsOptionVideoToWord");

            migrationBuilder.DropTable(
                name: "QuestionsOptionWordToVideo");

            migrationBuilder.DropTable(
                name: "QuestionsQA");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
