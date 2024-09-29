using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tenant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class menu_active_prop_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 731, DateTimeKind.Utc).AddTicks(8281),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 244, DateTimeKind.Utc).AddTicks(7900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 731, DateTimeKind.Utc).AddTicks(9396),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 244, DateTimeKind.Utc).AddTicks(8822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Menus",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 723, DateTimeKind.Utc).AddTicks(403),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 237, DateTimeKind.Utc).AddTicks(3278));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 723, DateTimeKind.Utc).AddTicks(1367),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 237, DateTimeKind.Utc).AddTicks(4102));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Menus");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 244, DateTimeKind.Utc).AddTicks(7900),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 731, DateTimeKind.Utc).AddTicks(8281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 244, DateTimeKind.Utc).AddTicks(8822),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 731, DateTimeKind.Utc).AddTicks(9396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Menus",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 237, DateTimeKind.Utc).AddTicks(3278),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 723, DateTimeKind.Utc).AddTicks(403));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Menus",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 28, 15, 23, 38, 237, DateTimeKind.Utc).AddTicks(4102),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 29, 10, 3, 31, 723, DateTimeKind.Utc).AddTicks(1367));
        }
    }
}
