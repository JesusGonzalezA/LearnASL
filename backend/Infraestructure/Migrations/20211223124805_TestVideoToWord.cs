﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructure.Migrations
{
    public partial class TestVideoToWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Test",
                table: "QuestionsOptionWordToVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_TestOptionWordToVideo_User",
                table: "TestsOptionWordToVideo");

            migrationBuilder.CreateTable(
                name: "TestsOptionVideoToWord",
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
                    table.PrimaryKey("PK_TestsOptionVideoToWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestOptionVideoToWord_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsOptionVideoToWord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoToGuess = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
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
                    table.PrimaryKey("PK_QuestionsOptionVideoToWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionVideoToWord_Test",
                        column: x => x.TestId,
                        principalTable: "TestsOptionVideoToWord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOptionVideoToWord_TestId",
                table: "QuestionsOptionVideoToWord",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestsOptionVideoToWord_UserId",
                table: "TestsOptionVideoToWord",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptionWordToVideo_Test",
                table: "QuestionsOptionWordToVideo",
                column: "TestId",
                principalTable: "TestsOptionWordToVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestsOptionWordToVideo_Users_UserId",
                table: "TestsOptionWordToVideo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptionWordToVideo_Test",
                table: "QuestionsOptionWordToVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_TestsOptionWordToVideo_Users_UserId",
                table: "TestsOptionWordToVideo");

            migrationBuilder.DropTable(
                name: "QuestionsOptionVideoToWord");

            migrationBuilder.DropTable(
                name: "TestsOptionVideoToWord");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Test",
                table: "QuestionsOptionWordToVideo",
                column: "TestId",
                principalTable: "TestsOptionWordToVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestOptionWordToVideo_User",
                table: "TestsOptionWordToVideo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
