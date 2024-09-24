using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fluentapi_edited2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 154, DateTimeKind.Utc).AddTicks(4896),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 126, DateTimeKind.Utc).AddTicks(6914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 154, DateTimeKind.Utc).AddTicks(5889),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 126, DateTimeKind.Utc).AddTicks(8151));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 167, DateTimeKind.Utc).AddTicks(3846),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(2273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 167, DateTimeKind.Utc).AddTicks(4899),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(3337));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 163, DateTimeKind.Utc).AddTicks(7646),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(3188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 163, DateTimeKind.Utc).AddTicks(8732),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(4284));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 126, DateTimeKind.Utc).AddTicks(6914),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 154, DateTimeKind.Utc).AddTicks(4896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 126, DateTimeKind.Utc).AddTicks(8151),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 154, DateTimeKind.Utc).AddTicks(5889));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(2273),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 167, DateTimeKind.Utc).AddTicks(3846));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(3337),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 167, DateTimeKind.Utc).AddTicks(4899));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(3188),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 163, DateTimeKind.Utc).AddTicks(7646));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(4284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 9, 39, 44, 163, DateTimeKind.Utc).AddTicks(8732));
        }
    }
}
