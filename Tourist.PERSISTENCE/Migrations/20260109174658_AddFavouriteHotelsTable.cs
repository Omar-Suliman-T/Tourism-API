using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tourist.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class AddFavouriteHotelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "whatWasGreat",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavouriteHotels",
                columns: table => new
                {
                    FavouriteHotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteHotels", x => x.FavouriteHotelId);
                    table.ForeignKey(
                        name: "FK_FavouriteHotels_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavouriteHotels_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteHotels_HotelId",
                table: "FavouriteHotels",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteHotels_UserId",
                table: "FavouriteHotels",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteHotels");

            migrationBuilder.DropColumn(
                name: "whatWasGreat",
                table: "Reviews");
        }
    }
}
