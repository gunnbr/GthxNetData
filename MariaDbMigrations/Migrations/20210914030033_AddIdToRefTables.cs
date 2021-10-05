using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MariaDbMigrations.Migrations
{
    public partial class AddIdToRefTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_YoutubeRef",
                table: "YoutubeRef");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThingiverseRef",
                table: "ThingiverseRef");

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "YoutubeRef",
                type: "varchar(191)",
                maxLength: 191,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(191)",
                oldMaxLength: 191)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql("ALTER TABLE YoutubeRef ADD Id int NOT NULL AUTO_INCREMENT PRIMARY KEY;");

            /*
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "YoutubeRef",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            */

            migrationBuilder.AlterColumn<int>(
                name: "Item",
                table: "ThingiverseRef",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.Sql("ALTER TABLE ThingiverseRef ADD Id int NOT NULL AUTO_INCREMENT PRIMARY KEY;");

            /*
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ThingiverseRef",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutubeRef",
                table: "YoutubeRef",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThingiverseRef",
                table: "ThingiverseRef",
                column: "Id");
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_YoutubeRef",
                table: "YoutubeRef");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThingiverseRef",
                table: "ThingiverseRef");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "YoutubeRef");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ThingiverseRef");

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "YoutubeRef",
                type: "varchar(191)",
                maxLength: 191,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(191)",
                oldMaxLength: 191,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Item",
                table: "ThingiverseRef",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutubeRef",
                table: "YoutubeRef",
                column: "Item");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThingiverseRef",
                table: "ThingiverseRef",
                column: "Item");
        }
    }
}
