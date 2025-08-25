using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_RoomStatus_EnumToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoomStatus",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Available",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoomStatus",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 20,
                oldDefaultValue: "Available");
        }
    }
}
