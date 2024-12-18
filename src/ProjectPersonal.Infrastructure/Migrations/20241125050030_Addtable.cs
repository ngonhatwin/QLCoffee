using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPersonal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__products__47027DF515DC49C9", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 255, nullable: false),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    full_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__B9BE370FB4ECFAE5", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 255, nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 255, nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__46596229562ED796", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__orders__user_id__3E52440B",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    token_id = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 255, nullable: false),
                    user_id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    refresh_token_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    expires_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_revoked = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__refresh___CB3C9E17F7FD50A1", x => x.token_id);
                    table.ForeignKey(
                        name: "FK__refresh_t__user___44FF419A",
                        column: x => x.token_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    order_item_id = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 255, nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 255, nullable: true),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_it__3764B6BC20574E91", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK__order_ite__order__412EB0B6",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__order_ite__produ__4222D4EF",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__users__AB6E61646C862CAE",
                table: "users",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__users__F3DBC572E85E12B4",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
