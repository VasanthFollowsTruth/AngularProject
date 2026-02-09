using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AngularProject.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "City", "Country", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PhoneNumber", "PostalCode", "State", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Street 1", "Chennai", "India", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@test.com", "John", false, "Doe", "+919999999999", "600001", "Tamil Nadu", null },
                    { 2, "Main Road", "New York", "USA", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@test.com", "Jane", false, "Smith", "+14155552671", "10001", "NY", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
