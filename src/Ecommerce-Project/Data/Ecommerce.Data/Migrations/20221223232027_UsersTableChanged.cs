namespace Ecommerce.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class UsersTableChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCards_ShoppingCardId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingCardId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCards",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCards_UserId",
                table: "ShoppingCards",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCards_AspNetUsers_UserId",
                table: "ShoppingCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCards_AspNetUsers_UserId",
                table: "ShoppingCards");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCards_UserId",
                table: "ShoppingCards");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ShoppingCards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCardId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingCardId",
                table: "AspNetUsers",
                column: "ShoppingCardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCards_ShoppingCardId",
                table: "AspNetUsers",
                column: "ShoppingCardId",
                principalTable: "ShoppingCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
