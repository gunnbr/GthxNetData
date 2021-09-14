using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlServerMigrations.Migrations
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
                type: "nvarchar(191)",
                maxLength: 191,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(191)",
                oldMaxLength: 191);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "YoutubeRef",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Item",
                table: "ThingiverseRef",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ThingiverseRef",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YoutubeRef",
                table: "YoutubeRef",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThingiverseRef",
                table: "ThingiverseRef",
                column: "Id");
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
                type: "nvarchar(191)",
                maxLength: 191,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(191)",
                oldMaxLength: 191,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Item",
                table: "ThingiverseRef",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

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
