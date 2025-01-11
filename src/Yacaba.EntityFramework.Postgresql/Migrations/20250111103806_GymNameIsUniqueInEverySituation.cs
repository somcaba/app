using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yacaba.EntityFramework.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class GymNameIsUniqueInEverySituation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_gym_name",
                table: "GYMS");

            migrationBuilder.CreateIndex(
                name: "ix_gym_name",
                table: "GYMS",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GYMS_id_organisation_fk",
                table: "GYMS",
                column: "id_organisation_fk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_gym_name",
                table: "GYMS");

            migrationBuilder.DropIndex(
                name: "IX_GYMS_id_organisation_fk",
                table: "GYMS");

            migrationBuilder.CreateIndex(
                name: "ix_gym_name",
                table: "GYMS",
                columns: new[] { "id_organisation_fk", "name" },
                unique: true);
        }
    }
}
