using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsCookTodayApi.Migrations
{
    /// <inheritdoc />
    public partial class deleteSlider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.RenameColumn(
                name: "AIPromptName",
                table: "AIPrompts",
                newName: "MyPromptsMaterials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyPromptsMaterials",
                table: "AIPrompts",
                newName: "AIPromptName");

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    SilderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.SilderId);
                });
        }
    }
}
