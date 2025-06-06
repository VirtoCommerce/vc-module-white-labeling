﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtoCommerce.WhiteLabeling.Data.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WhiteLabelingSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    OrganizationId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LogoUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    SecondaryLogoUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    FaviconUrl = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    FooterLinkListName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ThemePresetName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    StoreId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ModifiedBy = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteLabelingSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhiteLabelingSettings");
        }
    }
}
