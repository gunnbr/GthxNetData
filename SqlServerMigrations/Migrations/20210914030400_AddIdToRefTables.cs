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

/* SQL Server doesn't support changing the IDENTITY column
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
*/

            // So instead, create a temporary table the way
	    // we want it, copy the data over, then rename
	    // the temp table to the real name.
            migrationBuilder.Sql(@"
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'YoutubeTemp')
DROP TABLE YoutubeTemp;

CREATE TABLE YoutubeTemp(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Item] [nvarchar](191) NOT NULL,
    [Title] [nvarchar](255),
    [Count] [int],
    [Timestamp] [datetime2],
);
INSERT INTO YoutubeTemp SELECT * FROM YoutubeRef;
DROP TABLE YoutubeRef;
EXEC sp_rename 'YoutubeTemp', 'YoutubeRef';
            ");

/* SQL Server doesn't support it on the ThingiverseRef table either
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
*/

            // Same technique for the ThingiverseRef table
            migrationBuilder.Sql(@"
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'ThingiverseTemp')
DROP TABLE ThingiverseTemp;
CREATE TABLE ThingiverseTemp(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Item] [int] NOT NULL,
    [Title] [nvarchar](255),
    [Count] [int],
    [Timestamp] [datetime2],
);
INSERT INTO ThingiverseTemp(Item, Title, Count, Timestamp) SELECT Item, Title, Count, Timestamp FROM ThingiverseRef;
DROP TABLE ThingiverseRef;
EXEC sp_rename 'ThingiverseTemp', 'ThingiverseRef';
            ");
	    
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

/* SQL Server doesn't support changing or deleting the IDENTITY column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "YoutubeRef");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ThingiverseRef");
*/


            // So instead, create a temporary table the way
	    // we want it, copy the data over, then rename
	    // the temp table to the real name.
            migrationBuilder.Sql(@"
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'YoutubeTemp')
DROP TABLE YoutubeTemp;

CREATE TABLE YoutubeTemp(
    [Item] [nvarchar](191) NOT NULL,
    [Title] [nvarchar](255),
    [Count] [int],
    [Timestamp] [datetime2],
);
INSERT INTO YoutubeTemp(Item, Title, [Count], [Timestamp]) SELECT Item, Title, [Count], [Timestamp] FROM YoutubeRef;
DROP TABLE YoutubeRef;
EXEC sp_rename 'YoutubeTemp', 'YoutubeRef';

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'ThingiverseTemp')
DROP TABLE ThingiverseTemp;
CREATE TABLE ThingiverseTemp(
    [Item] [int] NOT NULL,
    [Title] [nvarchar](255),
    [Count] [int],
    [Timestamp] [datetime2],
);
INSERT INTO ThingiverseTemp(Item, Title, [Count], [Timestamp]) SELECT Item, Title, [Count], [Timestamp] FROM ThingiverseRef;
DROP TABLE ThingiverseRef;
EXEC sp_rename 'ThingiverseTemp', 'ThingiverseRef';
            ");
	    
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
