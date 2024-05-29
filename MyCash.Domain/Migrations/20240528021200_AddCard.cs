using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyCash.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bank_cards",
                columns: table => new
                {
                    card_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    card_name = table.Column<string>(type: "text", nullable: true),
                    cvv = table.Column<string>(type: "text", nullable: true),
                    number_card = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_cards", x => x.card_id);
                });

            migrationBuilder.CreateTable(
                name: "bank_card_account",
                columns: table => new
                {
                    account_id = table.Column<int>(type: "integer", nullable: false),
                    card_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_card_account", x => new { x.account_id, x.card_id });
                    table.ForeignKey(
                        name: "fk_bank_card_account_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bank_card_account_bank_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "bank_cards",
                        principalColumn: "card_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bank_card_account_card_id",
                table: "bank_card_account",
                column: "card_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_card_account");

            migrationBuilder.DropTable(
                name: "bank_cards");
        }
    }
}
