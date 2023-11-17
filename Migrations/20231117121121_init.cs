using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DoAn.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    img_path = table.Column<string>(type: "varchar(225)", maxLength: 225, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.author_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "book_owner",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.book_id, x.user_id });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.category_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    user_type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    username = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.user_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, nullable: false),
                    img_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    book_path = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ISBN = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    status = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.book_id);
                    table.ForeignKey(
                        name: "fk_b_at",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "author_id");
                    table.ForeignKey(
                        name: "fk_b_ct",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, nullable: true, defaultValueSql: "'0.00'"),
                    address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    sdt = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.invoice_id);
                    table.ForeignKey(
                        name: "pk_iv_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.book_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_ca_b",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "fk_ca_us",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice_detail",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValueSql: "'-1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.invoice_id, x.book_id });
                    table.ForeignKey(
                        name: "fk_odd_b",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "fk_odd_od",
                        column: x => x.invoice_id,
                        principalTable: "invoice",
                        principalColumn: "invoice_id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "fk_b_at_idx",
                table: "book",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "pk_b_ct_idx",
                table: "book",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "fk_ca_us_idx",
                table: "cart",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "pk_iv_user_idx",
                table: "invoice",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "pk_odd_b_idx",
                table: "invoice_detail",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "email_UNIQUE",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "username_UNIQUE",
                table: "user",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_owner");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "invoice_detail");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "invoice");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
