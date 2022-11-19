namespace eCommerceAPI.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class ApplicationUserAddressesDbSetChangedToOrderAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ApplicationUserAddresses_AddressId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserAddresses",
                table: "ApplicationUserAddresses");

            migrationBuilder.RenameTable(
                name: "ApplicationUserAddresses",
                newName: "OrderAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderAddresses",
                table: "OrderAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderAddresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "OrderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderAddresses_AddressId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderAddresses",
                table: "OrderAddresses");

            migrationBuilder.RenameTable(
                name: "OrderAddresses",
                newName: "ApplicationUserAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserAddresses",
                table: "ApplicationUserAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ApplicationUserAddresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "ApplicationUserAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
