using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.EventGateway.Migrations
{
    /// <inheritdoc />
    public partial class table_name_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Databases",
                table: "Databases");

            migrationBuilder.RenameTable(
                name: "Databases",
                newName: "MenuDatabases");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "MenuDatabases",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 26, 17, 35, 55, 645, DateTimeKind.Utc).AddTicks(2768),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 26, 17, 31, 53, 477, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "MenuDatabases",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 17, 35, 55, 645, DateTimeKind.Utc).AddTicks(3584),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 26, 17, 31, 53, 477, DateTimeKind.Utc).AddTicks(7445));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuDatabases",
                table: "MenuDatabases",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuDatabases",
                table: "MenuDatabases");

            migrationBuilder.RenameTable(
                name: "MenuDatabases",
                newName: "Databases");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Databases",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 26, 17, 31, 53, 477, DateTimeKind.Utc).AddTicks(6710),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 26, 17, 35, 55, 645, DateTimeKind.Utc).AddTicks(2768));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Databases",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 17, 31, 53, 477, DateTimeKind.Utc).AddTicks(7445),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 26, 17, 35, 55, 645, DateTimeKind.Utc).AddTicks(3584));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Databases",
                table: "Databases",
                column: "Id");
        }
    }
}
