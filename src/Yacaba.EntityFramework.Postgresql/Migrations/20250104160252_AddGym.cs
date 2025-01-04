using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Yacaba.EntityFramework.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class AddGym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contact",
                table: "ORGANISATIONS",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GYMS",
                columns: table => new
                {
                    id_gym_pk = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    image = table.Column<string>(type: "text", nullable: true),
                    contact = table.Column<string>(type: "text", nullable: true),
                    address_line1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    address_line2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    address_line3 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    address_npa = table.Column<string>(type: "text", nullable: true),
                    address_locality = table.Column<string>(type: "text", nullable: true),
                    address_coutry = table.Column<string>(type: "text", nullable: true),
                    location_latitude = table.Column<string>(type: "text", nullable: true),
                    location_longitude = table.Column<string>(type: "text", nullable: true),
                    is_official = table.Column<bool>(type: "boolean", nullable: false),
                    id_organisation_fk = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GYMS", x => x.id_gym_pk);
                    table.ForeignKey(
                        name: "fk_gym_organisation",
                        column: x => x.id_organisation_fk,
                        principalTable: "ORGANISATIONS",
                        principalColumn: "id_organisation_pk",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_gym_name",
                table: "GYMS",
                columns: new[] { "id_organisation_fk", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GYMS");

            migrationBuilder.DropColumn(
                name: "contact",
                table: "ORGANISATIONS");
        }
    }
}
