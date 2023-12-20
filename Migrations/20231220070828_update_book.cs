using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn.Migrations
{
    /// <inheritdoc />
    public partial class update_book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "book");

            migrationBuilder.DropColumn(
                name: "book_path",
                table: "book");

            migrationBuilder.DropColumn(
                name: "status",
                table: "book");

            migrationBuilder.AddColumn<double>(
                name: "rate",
                table: "book",
                type: "double",
                nullable: true,
                defaultValueSql: "'0'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rate",
                table: "book");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "book",
                type: "varchar(13)",
                maxLength: 13,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "book_path",
                table: "book",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "book",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
