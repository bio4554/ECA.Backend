using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECA.Backend.Data.Migrations
{
    public partial class UpdateRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_UserProfile_ProfileId",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_ProfileId",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "UserAccount");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProfile",
                newName: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserAccountId",
                table: "UserProfile",
                column: "UserAccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_UserAccount_UserAccountId",
                table: "UserProfile",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_UserAccount_UserAccountId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_UserAccountId",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                table: "UserProfile",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "UserAccount",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_ProfileId",
                table: "UserAccount",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_UserProfile_ProfileId",
                table: "UserAccount",
                column: "ProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
