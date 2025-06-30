using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cashbox.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCashboxSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashRegisterSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OpenedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OpenedByUserId = table.Column<int>(type: "integer", nullable: false),
                    ClosedByUserId = table.Column<int>(type: "integer", nullable: true),
                    InitialAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ExpectedAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CountedAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    Difference = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisterSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashRegisterMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MadeByUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisterMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashRegisterMovements_CashRegisterSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "CashRegisterSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashRegisterSessionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisterSessionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashRegisterSessionOrders_CashRegisterSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "CashRegisterSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterMovements_SessionId",
                table: "CashRegisterMovements",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisterSessionOrders_SessionId",
                table: "CashRegisterSessionOrders",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashRegisterMovements");

            migrationBuilder.DropTable(
                name: "CashRegisterSessionOrders");

            migrationBuilder.DropTable(
                name: "CashRegisterSessions");
        }
    }
}
