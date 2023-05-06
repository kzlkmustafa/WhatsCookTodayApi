using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsCookTodayApi.Migrations
{
    /// <inheritdoc />
    public partial class createDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MealOfDays",
                columns: table => new
                {
                    MealOfDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealOfDayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealOfDayRecipe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealOfDayPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealOfDays", x => x.MealOfDayId);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MyPrompts",
                columns: table => new
                {
                    MyPromptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyPrompts", x => x.MyPromptId);
                    table.ForeignKey(
                        name: "FK_MyPrompts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIPrompts",
                columns: table => new
                {
                    AIPromptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AIPromptName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AIPromptRecipe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MyPromptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIPrompts", x => x.AIPromptId);
                    table.ForeignKey(
                        name: "FK_AIPrompts_MyPrompts_MyPromptId",
                        column: x => x.MyPromptId,
                        principalTable: "MyPrompts",
                        principalColumn: "MyPromptId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AIPrompts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIPrompts_MyPromptId",
                table: "AIPrompts",
                column: "MyPromptId");

            migrationBuilder.CreateIndex(
                name: "IX_AIPrompts_UserId",
                table: "AIPrompts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyPrompts_UserId",
                table: "MyPrompts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIPrompts");

            migrationBuilder.DropTable(
                name: "MealOfDays");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "MyPrompts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
