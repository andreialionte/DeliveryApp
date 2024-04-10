using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "DeliveryAppSchema",
                table: "MenuItem",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "DeliveryAppSchema",
                table: "MenuItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "DeliveryAppSchema",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "Order",
                principalColumn: "OrderId");
        }
    }
}
