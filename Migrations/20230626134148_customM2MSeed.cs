using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalAssignmentWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class customM2MSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // deckard
            migrationBuilder.Sql("INSERT INTO CharacterMovie (CharactersId,MoviesId) VALUES (1,2)");
            migrationBuilder.Sql("INSERT INTO CharacterMovie (CharactersId,MoviesId) VALUES (1,4)");

            // Rachel
            migrationBuilder.Sql("INSERT INTO CharacterMovie (CharactersId,MoviesId) VALUES (2,2)");
            migrationBuilder.Sql("INSERT INTO CharacterMovie (CharactersId,MoviesId) VALUES (2,4)");

            // Roy
            migrationBuilder.Sql("INSERT INTO CharacterMovie (CharactersId,MoviesId) VALUES (3,4)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumn: "CharactersId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumn: "CharactersId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumn: "CharactersId",
                keyValue: 3);


            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumn: "MoviesId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumn: "MoviesId",
                keyValue: 4);

        }
    }
}
