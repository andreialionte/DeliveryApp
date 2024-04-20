using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Adding_FK_DeliveryAgent_To_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryAgentId",
                schema: "DeliveryAppSchema",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeliveryAgentId",
                schema: "DeliveryAppSchema",
                table: "Order",
                column: "DeliveryAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliveryAgent_DeliveryAgentId",
                schema: "DeliveryAppSchema",
                table: "Order",
                column: "DeliveryAgentId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "DeliveryAgent",
                principalColumn: "DeliveryAgentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliveryAgent_DeliveryAgentId",
                schema: "DeliveryAppSchema",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_DeliveryAgentId",
                schema: "DeliveryAppSchema",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeliveryAgentId",
                schema: "DeliveryAppSchema",
                table: "Order");
        }
    }
}
