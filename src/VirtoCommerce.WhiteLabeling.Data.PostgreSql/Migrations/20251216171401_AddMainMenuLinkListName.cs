using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtoCommerce.WhiteLabeling.Data.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddMainMenuLinkListName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainMenuLinkListName",
                table: "WhiteLabelingSettings",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainMenuLinkListName",
                table: "WhiteLabelingSettings");
        }
    }
}
