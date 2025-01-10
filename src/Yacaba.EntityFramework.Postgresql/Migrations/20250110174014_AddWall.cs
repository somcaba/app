using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Yacaba.EntityFramework.Postgresql.Migrations
{
    /// <inheritdoc />
    public partial class AddWall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WALLS",
                columns: table => new
                {
                    id_wall_pk = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    image = table.Column<string>(type: "text", nullable: false),
                    wall_type = table.Column<int>(type: "integer", nullable: false),
                    angle = table.Column<int>(type: "integer", nullable: false),
                    id_gym_fk = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WALLS", x => x.id_wall_pk);
                    table.ForeignKey(
                        name: "fk_wall_gym",
                        column: x => x.id_gym_fk,
                        principalTable: "GYMS",
                        principalColumn: "id_gym_pk",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_wall_name",
                table: "WALLS",
                columns: new[] { "id_gym_fk", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WALLS");
        }
    }
}
