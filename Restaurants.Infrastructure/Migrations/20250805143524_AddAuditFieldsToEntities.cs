using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFieldsToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Restaurants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Restaurants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Restaurants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedByAt",
                table: "Restaurants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Dishes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Dishes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Dishes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedByAt",
                table: "Dishes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LastModifiedByAt",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "LastModifiedByAt",
                table: "Dishes");
        }
    }
}
