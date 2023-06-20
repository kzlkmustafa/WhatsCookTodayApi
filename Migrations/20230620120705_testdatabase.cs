using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsCookTodayApi.Migrations
{
    /// <inheritdoc />
    public partial class testdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIPrompts_AspNetUsers_Id",
                table: "AIPrompts");

            migrationBuilder.DropIndex(
                name: "IX_AIPrompts_Id",
                table: "AIPrompts");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AIPrompts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AIPrompts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AIPrompts_UserId",
                table: "AIPrompts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AIPrompts_AspNetUsers_UserId",
                table: "AIPrompts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AIPrompts_AspNetUsers_UserId",
                table: "AIPrompts");

            migrationBuilder.DropIndex(
                name: "IX_AIPrompts_UserId",
                table: "AIPrompts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AIPrompts");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AIPrompts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AIPrompts_Id",
                table: "AIPrompts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AIPrompts_AspNetUsers_Id",
                table: "AIPrompts",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
