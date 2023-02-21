using Microsoft.EntityFrameworkCore.Migrations;

namespace QASite.Data.Migrations
{
    public partial class classes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionLike_Questions_QuestionId",
                table: "QuestionLike");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionLike_Users_UserId",
                table: "QuestionLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionLike",
                table: "QuestionLike");

            migrationBuilder.RenameTable(
                name: "QuestionLike",
                newName: "QuestionLikes");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionLike_UserId",
                table: "QuestionLikes",
                newName: "IX_QuestionLikes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionLikes",
                table: "QuestionLikes",
                columns: new[] { "QuestionId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionLikes_Questions_QuestionId",
                table: "QuestionLikes",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionLikes_Users_UserId",
                table: "QuestionLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionLikes_Questions_QuestionId",
                table: "QuestionLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionLikes_Users_UserId",
                table: "QuestionLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionLikes",
                table: "QuestionLikes");

            migrationBuilder.RenameTable(
                name: "QuestionLikes",
                newName: "QuestionLike");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionLikes_UserId",
                table: "QuestionLike",
                newName: "IX_QuestionLike_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionLike",
                table: "QuestionLike",
                columns: new[] { "QuestionId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionLike_Questions_QuestionId",
                table: "QuestionLike",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionLike_Users_UserId",
                table: "QuestionLike",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
