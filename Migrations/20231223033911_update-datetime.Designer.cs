﻿// <auto-generated />
using System;
using DoAn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoAn.Migrations
{
    [DbContext(typeof(CContext))]
    [Migration("20231223033911_update-datetime")]
    partial class updatedatetime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DoAn.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ImgPath")
                        .HasMaxLength(225)
                        .HasColumnType("varchar(225)")
                        .HasColumnName("img_path");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("AuthorId")
                        .HasName("PRIMARY");

                    b.ToTable("author", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("ImgPath")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("img_path");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<decimal?>("Price")
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("price");

                    b.Property<int?>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("quantity")
                        .HasDefaultValueSql("'0'");

                    b.Property<double?>("Rate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double")
                        .HasColumnName("rate")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("BookId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "AuthorId" }, "fk_b_at_idx");

                    b.HasIndex(new[] { "CategoryId" }, "pk_b_ct_idx");

                    b.ToTable("book", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.Cart", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("quantity")
                        .HasDefaultValueSql("'1'");

                    b.HasKey("BookId", "UserId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UserId" }, "fk_ca_us_idx");

                    b.ToTable("cart", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("CategoryId")
                        .HasName("PRIMARY");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.Efmigrationshistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("MigrationId")
                        .HasName("PRIMARY");

                    b.ToTable("__efmigrationshistory", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("invoice_id");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("address");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<string>("Sdt")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("sdt");

                    b.Property<decimal?>("TotalPrice")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("total_price")
                        .HasDefaultValueSql("'0.00'");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("InvoiceId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UserId" }, "pk_iv_user_idx");

                    b.ToTable("invoice", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.InvoiceDetail", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("int")
                        .HasColumnName("invoice_id");

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("quantity")
                        .HasDefaultValueSql("'1'");

                    b.Property<decimal?>("UnitPrice")
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("unit_price");

                    b.HasKey("InvoiceId", "BookId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "BookId" }, "pk_odd_b_idx");

                    b.ToTable("invoice_detail", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("review_id");

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<string>("Content")
                        .HasMaxLength(225)
                        .HasColumnType("varchar(225)")
                        .HasColumnName("content");

                    b.Property<double?>("Rate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double")
                        .HasColumnName("rate")
                        .HasDefaultValueSql("'1'");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("ReviewId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UserId" }, "fk_rv_u_idx");

                    b.HasIndex(new[] { "BookId" }, "pk_rv_b_idx");

                    b.ToTable("review", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Address");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("sdt");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("user_type");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Email" }, "email_UNIQUE")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "username_UNIQUE")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("DoAn.Models.Book", b =>
                {
                    b.HasOne("DoAn.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("fk_b_at");

                    b.HasOne("DoAn.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("fk_b_ct");

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DoAn.Models.Cart", b =>
                {
                    b.HasOne("DoAn.Models.Book", "Book")
                        .WithMany("Carts")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("fk_ca_b");

                    b.HasOne("DoAn.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("fk_ca_us");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoAn.Models.Invoice", b =>
                {
                    b.HasOne("DoAn.Models.User", "User")
                        .WithMany("Invoices")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("pk_iv_user");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoAn.Models.InvoiceDetail", b =>
                {
                    b.HasOne("DoAn.Models.Book", "Book")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("fk_odd_b");

                    b.HasOne("DoAn.Models.Invoice", "Invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId")
                        .IsRequired()
                        .HasConstraintName("fk_odd_od");

                    b.Navigation("Book");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("DoAn.Models.Review", b =>
                {
                    b.HasOne("DoAn.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAn.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoAn.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("DoAn.Models.Book", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("DoAn.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("DoAn.Models.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("DoAn.Models.User", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
