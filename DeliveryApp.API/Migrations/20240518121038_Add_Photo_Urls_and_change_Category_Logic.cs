using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Add_Photo_Urls_and_change_Category_Logic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                schema: "DeliveryAppSchema",
                table: "Restaurant");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "DeliveryAppSchema",
                table: "Restaurant",
                newName: "RestaurantPhotoUrl");

            migrationBuilder.RenameColumn(
                name: "Category",
                schema: "DeliveryAppSchema",
                table: "MenuItem",
                newName: "CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "MenuItemPhotoUrl",
                schema: "DeliveryAppSchema",
                table: "MenuItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "DeliveryAppSchema",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_CategoryId",
                schema: "DeliveryAppSchema",
                table: "MenuItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Category_CategoryId",
                schema: "DeliveryAppSchema",
                table: "MenuItem",
                column: "CategoryId",
                principalSchema: "DeliveryAppSchema",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Category_CategoryId",
                schema: "DeliveryAppSchema",
                table: "MenuItem");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "DeliveryAppSchema");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_CategoryId",
                schema: "DeliveryAppSchema",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "MenuItemPhotoUrl",
                schema: "DeliveryAppSchema",
                table: "MenuItem");

            migrationBuilder.RenameColumn(
                name: "RestaurantPhotoUrl",
                schema: "DeliveryAppSchema",
                table: "Restaurant",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "DeliveryAppSchema",
                table: "MenuItem",
                newName: "Category");

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryFee",
                schema: "DeliveryAppSchema",
                table: "Restaurant",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
