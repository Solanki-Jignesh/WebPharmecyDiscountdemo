using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPharmecyDiscountdemo.Migrations
{
    /// <inheritdoc />
    public partial class addUpdateTlbdiscountCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValueType",
                table: "TlbDiscountCodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ValueType",
                table: "TlbDiscountCodes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
