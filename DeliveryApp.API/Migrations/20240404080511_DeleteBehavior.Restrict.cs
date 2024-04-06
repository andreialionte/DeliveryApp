using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.API.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviorRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_MenuItem_MenuItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_MenuItem_MenuItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                column: "MenuItemId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_MenuItem_MenuItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_MenuItem_MenuItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                column: "MenuItemId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "MenuItem",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
