using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace konatus.data.Migrations
{
    public partial class AlterValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Stages",
                type: "varchar(100000)",
                maxLength: 100000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10000)",
                oldMaxLength: 10000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Stages",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 12, 37, 17, 858, DateTimeKind.Local).AddTicks(7855),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 22, 19, 18, 17, 878, DateTimeKind.Local).AddTicks(4036));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Maintenances",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 23, 12, 37, 17, 849, DateTimeKind.Local).AddTicks(4151),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 22, 19, 18, 17, 868, DateTimeKind.Local).AddTicks(5807));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Stages",
                type: "varchar(10000)",
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100000)",
                oldMaxLength: 100000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Stages",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 22, 19, 18, 17, 878, DateTimeKind.Local).AddTicks(4036),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 23, 12, 37, 17, 858, DateTimeKind.Local).AddTicks(7855));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Maintenances",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 9, 22, 19, 18, 17, 868, DateTimeKind.Local).AddTicks(5807),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 9, 23, 12, 37, 17, 849, DateTimeKind.Local).AddTicks(4151));
        }
    }
}
