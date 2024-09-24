using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fluentapi_changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 876, DateTimeKind.Utc).AddTicks(9848));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 126, DateTimeKind.Utc).AddTicks(8151),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 877, DateTimeKind.Utc).AddTicks(812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(2273),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 887, DateTimeKind.Utc).AddTicks(19));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(3337),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 887, DateTimeKind.Utc).AddTicks(980));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(3188),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 883, DateTimeKind.Utc).AddTicks(5004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(4284),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 883, DateTimeKind.Utc).AddTicks(5976));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 876, DateTimeKind.Utc).AddTicks(9848),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 126, DateTimeKind.Utc).AddTicks(6914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 877, DateTimeKind.Utc).AddTicks(812),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 126, DateTimeKind.Utc).AddTicks(8151));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 887, DateTimeKind.Utc).AddTicks(19),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(2273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "ConnectionPools",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 887, DateTimeKind.Utc).AddTicks(980),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 138, DateTimeKind.Utc).AddTicks(3337));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 883, DateTimeKind.Utc).AddTicks(5004),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(3188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateUTC",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 23, 17, 16, 6, 883, DateTimeKind.Utc).AddTicks(5976),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 24, 8, 57, 47, 134, DateTimeKind.Utc).AddTicks(4284));
        }
    }
}
