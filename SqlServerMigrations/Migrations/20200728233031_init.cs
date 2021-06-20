using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlServerMigrations.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factoid",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(maxLength: 255, nullable: true),
                    IsAre = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(maxLength: 512, nullable: true),
                    User = table.Column<string>(maxLength: 30, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factoid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FactoidHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(maxLength: 255, nullable: true),
                    Value = table.Column<string>(maxLength: 512, nullable: true),
                    User = table.Column<string>(maxLength: 30, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoidHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ref",
                columns: table => new
                {
                    Item = table.Column<string>(maxLength: 191, nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref", x => x.Item);
                });

            migrationBuilder.CreateTable(
                name: "Seen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(maxLength: 30, nullable: true),
                    Channel = table.Column<string>(maxLength: 30, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tell",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(maxLength: 60, nullable: true),
                    Recipient = table.Column<string>(maxLength: 60, nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tell", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThingiverseRef",
                columns: table => new
                {
                    Item = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThingiverseRef", x => x.Item);
                });

            migrationBuilder.CreateTable(
                name: "YoutubeRef",
                columns: table => new
                {
                    Item = table.Column<string>(maxLength: 191, nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeRef", x => x.Item);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factoid");

            migrationBuilder.DropTable(
                name: "FactoidHistory");

            migrationBuilder.DropTable(
                name: "Ref");

            migrationBuilder.DropTable(
                name: "Seen");

            migrationBuilder.DropTable(
                name: "Tell");

            migrationBuilder.DropTable(
                name: "ThingiverseRef");

            migrationBuilder.DropTable(
                name: "YoutubeRef");
        }
    }
}
