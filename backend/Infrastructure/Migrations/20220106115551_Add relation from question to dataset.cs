using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Addrelationfromquestiontodataset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DatasetItemId",
                table: "QuestionsQA",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DatasetItemId",
                table: "QuestionsOptionWordToVideo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DatasetItemId",
                table: "QuestionsOptionVideoToWord",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DatasetItemId",
                table: "QuestionsMimic",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsQA_DatasetItemId",
                table: "QuestionsQA",
                column: "DatasetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOptionWordToVideo_DatasetItemId",
                table: "QuestionsOptionWordToVideo",
                column: "DatasetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsOptionVideoToWord_DatasetItemId",
                table: "QuestionsOptionVideoToWord",
                column: "DatasetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsMimic_DatasetItemId",
                table: "QuestionsMimic",
                column: "DatasetItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_DatasetItem_QuestionMimicConfiguration",
                table: "QuestionsMimic",
                column: "DatasetItemId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_DatasetItem_QuestionOptionVideoToWordConfiguration",
                table: "QuestionsOptionVideoToWord",
                column: "DatasetItemId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_DatasetItem_QuestionOptionWordToVideoConfiguration",
                table: "QuestionsOptionWordToVideo",
                column: "DatasetItemId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_DatasetItem_QuestionQaConfiguration",
                table: "QuestionsQA",
                column: "DatasetItemId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_DatasetItem_QuestionMimicConfiguration",
                table: "QuestionsMimic");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_DatasetItem_QuestionOptionVideoToWordConfiguration",
                table: "QuestionsOptionVideoToWord");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_DatasetItem_QuestionOptionWordToVideoConfiguration",
                table: "QuestionsOptionWordToVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_DatasetItem_QuestionQaConfiguration",
                table: "QuestionsQA");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsQA_DatasetItemId",
                table: "QuestionsQA");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsOptionWordToVideo_DatasetItemId",
                table: "QuestionsOptionWordToVideo");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsOptionVideoToWord_DatasetItemId",
                table: "QuestionsOptionVideoToWord");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsMimic_DatasetItemId",
                table: "QuestionsMimic");

            migrationBuilder.DropColumn(
                name: "DatasetItemId",
                table: "QuestionsQA");

            migrationBuilder.DropColumn(
                name: "DatasetItemId",
                table: "QuestionsOptionWordToVideo");

            migrationBuilder.DropColumn(
                name: "DatasetItemId",
                table: "QuestionsOptionVideoToWord");

            migrationBuilder.DropColumn(
                name: "DatasetItemId",
                table: "QuestionsMimic");
        }
    }
}
