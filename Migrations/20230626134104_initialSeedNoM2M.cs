using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalAssignmentWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialSeedNoM2M : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "Gender", "Name", "Picture" },
                values: new object[,]
                {
                    { 1, "Blade Runner", "Male", "Rick Deckard", "https://static.wikia.nocookie.net/bladerunner/images/e/ed/Deck.jpg/revision/latest?cb=20220616043815" },
                    { 2, "N7FAA52318", "Female", "Rachael", "https://static.wikia.nocookie.net/bladerunner/images/c/ca/Rachael_Voight-Kampff_Test.jpg/revision/latest/scale-to-width-down/1000?cb=20220613123504" },
                    { 3, "N6MAA10816", "Male", "Roy Batty", "https://static.wikia.nocookie.net/bladerunner/images/c/c6/Roy_Batty_new.jpg/revision/latest?cb=20190301233437" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Contains the movies of the Dune franchise", "Dune" },
                    { 2, "Contains the movies of the Blade Runner franchise", "Blade Runner" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "MovieTitle", "Picture", "ReleaseYear", "Trailer" },
                values: new object[,]
                {
                    { 1, "Denis Villeneuve", 1, "Sci-fi", "Dune", "https://cdn.entries.clios.com/styles/clio_aotw_ems_image_details_retina/s3/entry_attachments/image/72/2297/22197/122278/noif6bM212YhBupvTTAoRBZPy7rnN7gLiFiwUeVaSBg.jpg", 2017, "https://www.youtube.com/watch?v=8g18jFHCLXk" },
                    { 2, "Denis Villeneuve", 2, "Sci-fi", "Blade Runner 2049", "https://alternativemovieposters.com/wp-content/uploads/2022/11/Matt-Ferguson_BladeRunner.jpg", 2021, "https://www.youtube.com/watch?v=gCcx85zbxz4" },
                    { 3, "Denis Villeneuve", 1, "Sci-fi", "Dune: Part Two", "https://mlpnk72yciwc.i.optimole.com/cqhiHLc.IIZS~2ef73/w:auto/h:auto/q:75/https://bleedingcool.com/wp-content/uploads/2023/05/DUNE2_VERT_Tsr_2764x4096_DOM_REV.jpg", 2023, "https://www.youtube.com/watch?v=Way9Dexny3w" },
                    { 4, "Ridley Scott", 2, "Sci-fi", "Blade Runner", "https://i.redd.it/lg5vx7dnwkd51.jpg", 1982, "https://www.youtube.com/watch?v=eogpIG53Cis" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Franchises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Franchises",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
