using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCash.Domain.Migrations
{
    /// <inheritdoc />
    public partial class dataTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "user_account",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "user_account",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "account_transaction",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "account_transaction",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "account_transaction");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "account_transaction");
        }
    }
}
