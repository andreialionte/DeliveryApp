using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.API.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemFKErrorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.AlterColumn<int>(
                name: "OrderItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                column: "OrderId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.AlterColumn<int>(
                name: "OrderItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderItemId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                column: "OrderItemId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
