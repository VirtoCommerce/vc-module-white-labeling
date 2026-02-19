using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtoCommerce.WhiteLabeling.Data.SqlServer.Migrations
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
                type: "nvarchar(256)",
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
