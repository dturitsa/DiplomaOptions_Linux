using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DiplomaWebSite.Migrations.Options
{
    public partial class OptionsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    OptionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isActive = table.Column<bool>(nullable: false),
                    title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.OptionId);
                });
            migrationBuilder.CreateTable(
                name: "YearTerm",
                columns: table => new
                {
                    YearTermId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isDefault = table.Column<bool>(nullable: false),
                    term = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearTerm", x => x.YearTermId);
                });
            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstChoiceOptionId = table.Column<int>(nullable: false),
                    FourthChoiceOptionId = table.Column<int>(nullable: false),
                    SecondChoiceOptionId = table.Column<int>(nullable: false),
                    SelectionDate = table.Column<DateTime>(nullable: false),
                    StudentFirstName = table.Column<string>(nullable: false),
                    StudentId = table.Column<string>(nullable: false),
                    StudentLastname = table.Column<string>(nullable: false),
                    ThirdChoiceOptionId = table.Column<int>(nullable: false),
                    YearTermId = table.Column<int>(nullable: false),
                    optionsOptionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_Choice_YearTerm_YearTermId",
                        column: x => x.YearTermId,
                        principalTable: "YearTerm",
                        principalColumn: "YearTermId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Choice_Option_optionsOptionId",
                        column: x => x.optionsOptionId,
                        principalTable: "Option",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Choice");
            migrationBuilder.DropTable("YearTerm");
            migrationBuilder.DropTable("Option");
        }
    }
}
